using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic.Models;
using Octokit;

namespace GitHubReleaseNotes.Logic
{
    public static class RepositoryHelper
    {
        private const int deltaSeconds = 30;

        private static readonly GitHubClient Client = new GitHubClient(new ProductHeaderValue("GitHubReleaseNotes"));

        internal static async Task<IEnumerable<ReleaseInfo>> GetReleaseInfoAsync(string repositoryPath)
        {
            var repo = new LibGit2Sharp.Repository(repositoryPath);
            string url = repo.Network.Remotes.First(r => r.Name == "origin").Url;

            Console.WriteLine($"Analyzing Git Repository at '{repositoryPath}'");
            var orderedReleaseInfos = GetOrderedReleaseInfos(repo);

            Console.WriteLine($"Getting Issues and PullRequests from '{url}'");
            (List<Issue> issuesFromProject, List<PullRequest> pullRequestsFromProject) = await GetAllIssuesAndPullRequestsAsync(url);

            // Loop all orderedReleaseInfos and add the correct Pull Requests and Issues
            int idx = 0;
            foreach (var releaseInfo in orderedReleaseInfos)
            {
                bool issueTimeIsLessThenReleaseTime(DateTimeOffset releaseTime, DateTimeOffset? issueClosedTime) => issueClosedTime < releaseTime.AddSeconds(deltaSeconds);
                bool issueTimeIsGreaterThenPreviousReleaseTime(DateTimeOffset? issueClosedTime) => idx > 0 ? issueClosedTime > orderedReleaseInfos[idx - 1].When.AddSeconds(deltaSeconds) : true;
                bool issueLinkedToRelease(DateTimeOffset? issueClosedAtTime) => issueTimeIsLessThenReleaseTime(releaseInfo.When, issueClosedAtTime) && issueTimeIsGreaterThenPreviousReleaseTime(issueClosedAtTime);

                // Process Issues
                var issuesForThisTag = issuesFromProject.Where(issue => issueLinkedToRelease(issue.ClosedAt));
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
                var pullsForThisTag = pullRequestsFromProject.Where(issue => issueLinkedToRelease(issue.ClosedAt));
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

                idx++;
            }

            return orderedReleaseInfos.OrderByDescending(r => r.Version);
        }

        private static List<ReleaseInfo> GetOrderedReleaseInfos(LibGit2Sharp.Repository repo)
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
                FriendlyName = "next",
                When = DateTimeOffset.Now
            });

            return orderedReleaseInfos;
        }

        private static async Task<(List<Issue> Issues, List<PullRequest> PullRequests)> GetAllIssuesAndPullRequestsAsync(string url)
        {
            (string owner, string project) = GetOwnerAndProject(url);

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed Issues
            var closedIssuesRequest = new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending,
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed
            };
            var issuesFromProject = (await Client.Issue.GetAllForRepository(owner, project, closedIssuesRequest)).Where(issue => issue.PullRequest == null).ToList();

            // Do a request to GitHub using Octokit.GitHubClient to get all Closed and Merged Pull Requests
            var closedPullRequestsRequest = new PullRequestRequest
            {
                SortDirection = SortDirection.Ascending,
                State = ItemStateFilter.Closed
            };
            var pullRequestsFromProject = (await Client.PullRequest.GetAllForRepository(owner, project, closedPullRequestsRequest)).Where(pull => pull.Merged).ToList();

            return (issuesFromProject, pullRequestsFromProject);
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
                return version.Major * 1000000000L + version.Minor * 1000000L + version.Build * 1000L + (version.Revision > 0 ? version.Revision : 0);
            }

            return null;
        }
    }
}