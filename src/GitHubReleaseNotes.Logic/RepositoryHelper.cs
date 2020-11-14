using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic.Models;
using Octokit;

namespace GitHubReleaseNotes.Logic
{
    public class RepositoryHelper
    {
        private const int DeltaSeconds = 30;

        private readonly IConfiguration _configuration;

        public RepositoryHelper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        internal async Task<IEnumerable<ReleaseInfo>> GetReleaseInfoAsync()
        {
            var repository = new LibGit2Sharp.Repository(_configuration.RepositoryPath);
            string origin = repository.Network.Remotes.First(r => r.Name == "origin").Url;
            string url = !origin.EndsWith(".git") ? $"{origin}.git" : origin;

            Console.WriteLine($"Analyzing Git Repository at '{new FileInfo(_configuration.RepositoryPath).FullName}'");
            var orderedReleaseInfos = GetOrderedReleaseInfos(repository);

            Console.WriteLine($"Getting Issues and Pull Requests from '{url}'");
            var result = await GetAllIssuesAndPullRequestsAsync(url).ConfigureAwait(false);

            bool IssueTimeIsLessThenReleaseTime(DateTimeOffset releaseTime, DateTimeOffset? issueClosedTime)
                => issueClosedTime < releaseTime.AddSeconds(DeltaSeconds);

            bool IssueTimeIsGreaterThenPreviousReleaseTime(int idx, DateTimeOffset? issueClosedTime) =>
                idx <= 0 || issueClosedTime > orderedReleaseInfos[idx - 1].When.AddSeconds(DeltaSeconds);

            bool IssueLinkedToRelease(int idx, ReleaseInfo releaseInfo, DateTimeOffset? issueClosedAtTime) =>
                IssueTimeIsLessThenReleaseTime(releaseInfo.When, issueClosedAtTime) && IssueTimeIsGreaterThenPreviousReleaseTime(idx, issueClosedAtTime);

            // Loop all orderedReleaseInfos and add the correct Pull Requests and Issues
            foreach (var x in orderedReleaseInfos.Select((releaseInfo, index) => new { index, releaseInfo }))
            {
                // Process only Issues
                var issuesForThisTag = result.Issues.Where(issue => issue.PullRequest == null && IssueLinkedToRelease(x.index, x.releaseInfo, issue.ClosedAt));
                var issueInfos = issuesForThisTag.Select(issue => new IssueInfo
                {
                    Number = issue.Number,
                    IsPulRequest = false,
                    IssueUrl = issue.HtmlUrl,
                    Title = issue.Title,
                    User = issue.User.Login,
                    UserUrl = issue.User.HtmlUrl,
                    Labels = issue.Labels.Select(label => label.Name).ToArray()
                });

                // Process PullRequests
                var pullsForThisTag = result.PullRequests.Where(pullRequest => IssueLinkedToRelease(x.index, x.releaseInfo, pullRequest.ClosedAt));
                var pullInfos = pullsForThisTag.Select(pull => new IssueInfo
                {
                    Number = pull.Number,
                    IsPulRequest = true,
                    IssueUrl = pull.HtmlUrl,
                    Title = pull.Title,
                    User = pull.User.Login,
                    UserUrl = pull.User.HtmlUrl,
                    Labels = result.Issues
                        .First(issue => issue.Number == pull.Number).Labels // Get the labels from the Issues (because this is not present in the 'PullRequest')
                        .Select(label => label.Name)
                        .ToArray()
                });

                bool ExcludeIssue(string[] labels) => _configuration.ExcludeLabels != null &&
                                                      _configuration.ExcludeLabels.Any(s => labels.Contains(s, StringComparer.OrdinalIgnoreCase));

                var allIssues = issueInfos.Union(pullInfos)
                    .Distinct()
                    .Where(issueInfo => !ExcludeIssue(issueInfo.Labels));

                x.releaseInfo.IssueInfos = allIssues.OrderByDescending(issue => issue.IsPulRequest).ThenBy(issue => issue.Number).ToList();
            }

            if (_configuration.SkipEmptyReleases)
            {
                orderedReleaseInfos = orderedReleaseInfos.Where(r => r.IssueInfos.Count > 0).ToList();
            }

            return orderedReleaseInfos.OrderByDescending(r => r.Version);
        }

        private List<ReleaseInfo> GetOrderedReleaseInfos(LibGit2Sharp.Repository repo)
        {
            var orderedReleaseInfos = repo.Tags

                // Convert Tag into ReleaseInfo
                .Select(tag => new ReleaseInfo
                {
                    Version = GetVersionAsLong(tag.FriendlyName) ?? 0,
                    FriendlyName = tag.FriendlyName,
                    When = tag.Target is LibGit2Sharp.Commit commit ? commit.Committer.When : DateTimeOffset.MinValue
                })

                // Skip invalid versions
                .Where(tag => tag.Version > 0)

                // Order by the version
                .OrderBy(tag => tag.Version)
                .ToList();

            // Add the `next` version
            orderedReleaseInfos.Add(new ReleaseInfo
            {
                Version = long.MaxValue,
                FriendlyName = _configuration.Version,
                When = DateTimeOffset.Now
            });

            return orderedReleaseInfos;
        }

        private async Task<IssuesAndPullRequestsModel> GetAllIssuesAndPullRequestsAsync(string url)
        {
            GetOwnerAndProject(url, out string owner, out string project);

            var client = GitHubClientFactory.CreateClient(_configuration, owner);

            var miscellaneousRateLimit = await client.Miscellaneous.GetRateLimits();

            if (miscellaneousRateLimit.Resources.Core.Remaining < 50)
            {
                throw new Exception($"You have only {miscellaneousRateLimit.Resources.Core.Remaining} Core Requests remaining.");
            }

            return new IssuesAndPullRequestsModel
            {
                Issues = await GetIssuesForRepositoryAsync(client, owner, project).ConfigureAwait(false),
                PullRequests = await GetMergedPullRequestsForRepositoryAsync(client, owner, project).ConfigureAwait(false)
            };
        }

        private async Task<ICollection<Issue>> GetIssuesForRepositoryAsync(IGitHubClient client, string owner, string name)
        {
            var allIssues = new List<Issue>();

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed Issues (this does also include Closed and Merged Pull Requests)
            var closedIssuesRequest = new RepositoryIssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed
            };

            // Return all Closed issues
            return (await client.Issue.GetAllForRepository(owner, name, closedIssuesRequest).ConfigureAwait(false)).OrderBy(i => i.Id).ToList().AsReadOnly();
        }

        private async Task<ICollection<PullRequest>> GetMergedPullRequestsForRepositoryAsync(IGitHubClient client, string owner, string name)
        {
            var allPullRequests = new List<PullRequest>();

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed Pull Requests
            var closedPullRequestsRequest = new PullRequestRequest
            {
                //SortDirection = SortDirection.Ascending,
                State = ItemStateFilter.Closed
            };

            // Return only Closes and Merged PullRequests
            return (await client.PullRequest.GetAllForRepository(owner, name, closedPullRequestsRequest).ConfigureAwait(false))
                .Where(pull => pull.Merged)
                .OrderBy(pull => pull.Id)
                .ToList()
                .AsReadOnly();
        }

        private static void GetOwnerAndProject(string url, out string owner, out string project)
        {
            var regex = new Regex(@"^https:\/\/github.com\/(?<owner>.+)\/(?<project>.+).git$", RegexOptions.Compiled);

            owner = regex.Match(url).Groups["owner"].Value;
            project = regex.Match(url).Groups["project"].Value;
        }

        private static long? GetVersionAsLong(string friendlyName)
        {
            var versionAsString = new string(friendlyName.Where(c => char.IsDigit(c) || c == '.').ToArray());
            if (Version.TryParse(versionAsString, out Version version))
            {
                return version.Major * 1000000000L + version.Minor * 1000000L + (version.Build > 0 ? version.Build : 0) * 1000L + (version.Revision > 0 ? version.Revision : 0);
            }

            return null;
        }
    }
}