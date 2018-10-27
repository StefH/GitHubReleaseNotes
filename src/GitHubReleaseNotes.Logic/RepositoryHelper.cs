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
        private static readonly Regex Regex = new Regex(@"^https:\/\/github.com\/(?<owner>.+)\/(?<project>.+).git$", RegexOptions.Compiled);
        private static readonly GitHubClient Client = new GitHubClient(new ProductHeaderValue("GitHubReleaseNotes"));

        internal static async Task<IEnumerable<ReleaseInfo>> GetReleaseInfoAsync(string path)
        {
            using (var repo = new LibGit2Sharp.Repository(path))
            {
                (string owner, string project) = GetOwnerAndProduct(repo);

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

                // Add the `next` vesion
                orderedReleaseInfos.Add(new ReleaseInfo
                {
                    Version = long.MaxValue,
                    FriendlyName = "next",
                    When = DateTimeOffset.Now
                });

                // Do a request to GitHub using Octokit.GitHubClient
                var closedIssuesRequest = new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending,
                    Filter = IssueFilter.All,
                    State = ItemStateFilter.Closed
                };
                var issuesFromProject = (await Client.Issue.GetAllForRepository(owner, project, closedIssuesRequest)).ToList();

                // Loop all orderedReleaseInfos and add the correct Pull Requests and Issues
                int idx = 0;
                foreach (var releaseInfo in orderedReleaseInfos)
                {
                    var previousReleaseInfo = idx > 0 ? orderedReleaseInfos[idx - 1] : null;
                    var issuesForThisTag = issuesFromProject.Where(issue => issue.ClosedAt < releaseInfo.When && (previousReleaseInfo == null || issue.ClosedAt > previousReleaseInfo.When));

                    releaseInfo.IssueInfos = issuesForThisTag.Select(issue => new IssueInfo
                    {
                        Id = issue.Number,
                        IsPulRequest = issue.PullRequest != null,
                        IssueUrl = issue.HtmlUrl,
                        Title = issue.Title,
                        User = issue.User.Login,
                        UserUrl = issue.User.HtmlUrl
                    }).OrderByDescending(issue => issue.IsPulRequest).ThenBy(issue => issue.Id).ToList();

                    idx++;
                }

                return orderedReleaseInfos.OrderByDescending(r => r.Version);
            }
        }

        private static (string owner, string project) GetOwnerAndProduct(LibGit2Sharp.Repository repo)
        {
            string url = repo.Network.Remotes.First(r => r.Name == "origin").Url;

            return (Regex.Match(url).Groups["owner"].Value, Regex.Match(url).Groups["project"].Value);
        }

        private static long? GetVersionAsLong(string friendlyName)
        {
            if (Version.TryParse(friendlyName, out Version version))
            {
                return version.Major * 1000000000L + version.Minor * 1000000L + version.Build * 1000L + version.Revision;
            }

            return null;
        }
    }
}