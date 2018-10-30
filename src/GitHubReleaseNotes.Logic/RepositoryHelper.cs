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

        private readonly Configuration _configuration;
        private readonly GitHubClient _client;

        public RepositoryHelper(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _client = new GitHubClient(new ProductHeaderValue("GitHubReleaseNotes"));
        }

        internal async Task<IEnumerable<ReleaseInfo>> GetReleaseInfoAsync()
        {
            var repository = new LibGit2Sharp.Repository(_configuration.RepositoryPath);
            string url = repository.Network.Remotes.First(r => r.Name == "origin").Url;

            Console.WriteLine($"Analyzing Git Repository at '{new FileInfo(_configuration.RepositoryPath).FullName}'");
            var orderedReleaseInfos = GetOrderedReleaseInfos(repository);

            Console.WriteLine($"Getting Issues and Pull Requests from '{url}'");
            var (issuesFromProject, pullRequestsFromProject) = await GetAllIssuesAndPullRequestsAsync(url);

            bool IssueTimeIsLessThenReleaseTime(DateTimeOffset releaseTime, DateTimeOffset? issueClosedTime) => issueClosedTime < releaseTime.AddSeconds(DeltaSeconds);
            bool IssueTimeIsGreaterThenPreviousReleaseTime(int idx, DateTimeOffset? issueClosedTime) => idx <= 0 || issueClosedTime > orderedReleaseInfos[idx - 1].When.AddSeconds(DeltaSeconds);
            bool IssueLinkedToRelease(int idx, ReleaseInfo releaseInfo, DateTimeOffset? issueClosedAtTime) => IssueTimeIsLessThenReleaseTime(releaseInfo.When, issueClosedAtTime) && IssueTimeIsGreaterThenPreviousReleaseTime(idx, issueClosedAtTime);

            // Loop all orderedReleaseInfos and add the correct Pull Requests and Issues
            int index = 0;
            foreach (var releaseInfo in orderedReleaseInfos)
            {
                // Process Issues
                var issuesForThisTag = issuesFromProject.Where(issue => IssueLinkedToRelease(index, releaseInfo, issue.ClosedAt));
                var issueInfos = issuesForThisTag.Select(issue => new IssueInfo
                {
                    Number = issue.Number,
                    IsPulRequest = false,
                    IssueUrl = issue.HtmlUrl,
                    Title = issue.Title,
                    User = issue.User.Login,
                    UserUrl = issue.User.HtmlUrl
                });

                // Process PullRequests
                var pullsForThisTag = pullRequestsFromProject.Where(issue => IssueLinkedToRelease(index, releaseInfo, issue.ClosedAt));
                var pullInfos = pullsForThisTag.Select(pull => new IssueInfo
                {
                    Number = pull.Number,
                    IsPulRequest = true,
                    IssueUrl = pull.HtmlUrl,
                    Title = pull.Title,
                    User = pull.User.Login,
                    UserUrl = pull.User.HtmlUrl
                });

                var allIssues = issueInfos.Union(pullInfos).Distinct();
                releaseInfo.IssueInfos = allIssues.OrderByDescending(issue => issue.IsPulRequest).ThenBy(issue => issue.Number).ToList();

                index++;
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

        private async Task<(List<Issue> Issues, List<PullRequest> PullRequests)> GetAllIssuesAndPullRequestsAsync(string url)
        {
            (string owner, string project) = GetOwnerAndProject(url);

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed Issues
            var closedIssuesRequest = new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending,
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed
            };
            var issuesFromRepository = (await _client.Issue.GetAllForRepository(owner, project, closedIssuesRequest)).Where(issue => issue.PullRequest == null).ToList();

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed and Merged Pull Requests
            var closedPullRequestsRequest = new PullRequestRequest
            {
                SortDirection = SortDirection.Ascending,
                State = ItemStateFilter.Closed
            };
            var pullRequestsFromRepository = (await _client.PullRequest.GetAllForRepository(owner, project, closedPullRequestsRequest)).Where(pull => pull.Merged).ToList();

            return (issuesFromRepository, pullRequestsFromRepository);
        }

        private static (string owner, string project) GetOwnerAndProject(string url)
        {
            var regex = new Regex(@"^https:\/\/github.com\/(?<owner>.+)\/(?<project>.+).git$", RegexOptions.Compiled);
            return (regex.Match(url).Groups["owner"].Value, regex.Match(url).Groups["project"].Value);
        }

        private static long? GetVersionAsLong(string friendlyName)
        {
            if (Version.TryParse(friendlyName, out Version version))
            {
                return version.Major * 1000000000L + version.Minor * 1000000L + (version.Build > 0 ? version.Build : 0) * 1000L + (version.Revision > 0 ? version.Revision : 0);
            }

            return null;
        }
    }
}