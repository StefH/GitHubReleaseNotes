using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic.Extensions;
using GitHubReleaseNotes.Logic.Models;
using GitReader.Structures;
using Octokit;

namespace GitHubReleaseNotes.Logic;

public class RepositoryHelper
{
    private const int DeltaSeconds = 30;

    private static readonly Regex OwnerAndProjectRegex = new("(^https:\\/\\/github\\.com\\/(?<ownerHttps>.+)\\/(?<projectHttps>.+)\\.git)|(^git@github\\.com:(?<ownerSSH>.+)\\/(?<projectSSH>.+)\\.git$)$", RegexOptions.Compiled);

    private readonly IConfiguration _configuration;

    public RepositoryHelper(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    internal async Task<IEnumerable<ReleaseInfo>> GetReleaseInfoAsync()
    {
        using var repository = await GitReader.Repository.Factory.OpenStructureAsync(_configuration.RepositoryPath);

        var originUrl = repository.RemoteUrls.First(r => r.Key == "origin").Value;
        var gitUrl = !originUrl.EndsWith(".git") ? $"{originUrl}.git" : originUrl;
        var headBranchName = repository.Head?.Name ?? throw new InvalidOperationException("The Head branch has no name.");

        Console.WriteLine($"Analyzing Git Repository at '{new FileInfo(_configuration.RepositoryPath).FullName}'");
        var orderedReleaseInfos = await repository.GetOrderedReleaseInfosAsync(_configuration.Version);

        Console.WriteLine($"Getting Issues and Pull Requests from '{gitUrl}'");
        var result = await GetAllIssuesAndPullRequestsAsync(gitUrl, headBranchName).ConfigureAwait(false);

        bool IssueTimeIsLessThenReleaseTime(DateTimeOffset releaseTime, DateTimeOffset? issueClosedTime)
            => issueClosedTime < releaseTime.AddSeconds(DeltaSeconds);

        bool IssueTimeIsGreaterThenPreviousReleaseTime(IReadOnlyList<ReleaseInfo> releaseInfos, int idx, DateTimeOffset? issueClosedTime) =>
            idx <= 0 || issueClosedTime > releaseInfos[idx - 1].When.AddSeconds(DeltaSeconds);

        bool IssueLinkedToRelease(IReadOnlyList<ReleaseInfo> releaseInfos, int idx, ReleaseInfo releaseInfo, DateTimeOffset? issueClosedAtTime) =>
            IssueTimeIsLessThenReleaseTime(releaseInfo.When, issueClosedAtTime) && IssueTimeIsGreaterThenPreviousReleaseTime(releaseInfos, idx, issueClosedAtTime);

        bool ExcludeIssue(string[] labels) =>
            _configuration.ExcludeLabels != null && _configuration.ExcludeLabels.Any(s => labels.Contains(s, StringComparer.OrdinalIgnoreCase));

        // Loop all orderedReleaseInfos and add the correct Pull Requests and Issues
        foreach (var x in orderedReleaseInfos.Select((releaseInfo, index) => new { index, releaseInfo }))
        {
            // Process only Issues
            var releaseInfos = orderedReleaseInfos; // Fix: "Captured variable is modified in outer scope"
            var issuesForThisTag = result.Issues.Where(issue => issue.PullRequest == null && IssueLinkedToRelease(releaseInfos, x.index, x.releaseInfo, issue.ClosedAt));
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
            var pullsForThisTag = result.PullRequests.Where(pullRequest => IssueLinkedToRelease(releaseInfos, x.index, x.releaseInfo, pullRequest.ClosedAt));

            var pullInfos = pullsForThisTag.Select(pull =>
            {
                var labels = pull.Labels.Select(l => l.Name);

                // Get the labels from the Issues (if present). Because these are not present in the 'PullRequest' ?
                var relatedIssue = result.Issues.FirstOrDefault(issue => issue.Number == pull.Number);
                if (relatedIssue != null)
                {
                    labels = relatedIssue.Labels.Select(label => label.Name);
                }

                return new IssueInfo
                {
                    Number = pull.Number,
                    IsPulRequest = true,
                    IssueUrl = pull.HtmlUrl,
                    Title = pull.Title,
                    User = pull.User.Login,
                    UserUrl = pull.User.HtmlUrl,
                    Labels = labels.ToArray()
                };
            });

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

    private async Task<IssuesAndPullRequestsModel> GetAllIssuesAndPullRequestsAsync(string gitUrl, string headBranchName)
    {
        GetRepositorySettingsOrThrowException(gitUrl, headBranchName, out var repositorySettings);

        //var miscellaneousRateLimit = await client.Miscellaneous.GetRateLimits();
        //if (miscellaneousRateLimit.Resources.Core.Remaining < 21)
        //{
        //    throw new Exception($"You have only {miscellaneousRateLimit.Resources.Core.Remaining} Core Requests remaining.");
        //}

        var issuesTask = GetIssuesForRepositoryAsync(repositorySettings);
        var pullRequestsTask = GetMergedPullRequestsForRepositoryAsync(repositorySettings);

        await Task.WhenAll(issuesTask, pullRequestsTask).ConfigureAwait(false);

        return new IssuesAndPullRequestsModel
        {
            Issues = await issuesTask,
            PullRequests = await pullRequestsTask
        };
    }

    private async Task<ICollection<Issue>> GetIssuesForRepositoryAsync(RepositorySettings repositorySettings)
    {
        var client = GitHubClientFactory.CreateClient(_configuration, repositorySettings.Owner);

        // Do a request to GitHub using Octokit.GitHubClient to get all Closed Issues (this does also include Closed and Merged Pull Requests)
        var closedIssuesRequest = new RepositoryIssueRequest
        {
            Filter = IssueFilter.All,
            State = ItemStateFilter.Closed
        };

        // Return all Closed issues
        return (await client.Issue.GetAllForRepository(repositorySettings.Owner, repositorySettings.Name, closedIssuesRequest).ConfigureAwait(false))
            .OrderBy(i => i.Id)
            .ToList();
    }

    private async Task<ICollection<PullRequest>> GetMergedPullRequestsForRepositoryAsync(RepositorySettings repositorySettings)
    {
        var client = GitHubClientFactory.CreateClient(_configuration, repositorySettings.Owner);

        // Do a request to GitHub using Octokit.GitHubClient to get all Closed Pull Requests
        var closedPullRequestsRequest = new PullRequestRequest
        {
            //SortDirection = SortDirection.Ascending,
            State = ItemStateFilter.Closed,
            Base = repositorySettings.HeadBranch
        };

        // Return only Closed and Merged PullRequests
        return (await client.PullRequest.GetAllForRepository(repositorySettings.Owner, repositorySettings.Name, closedPullRequestsRequest).ConfigureAwait(false))
            .Where(pull => pull.Merged)
            .OrderBy(pull => pull.Id)
            .ToList()
            .AsReadOnly();
    }

    private static void GetRepositorySettingsOrThrowException(string url, string headBranchName, out RepositorySettings repositorySettings)
    {
        var groups = OwnerAndProjectRegex.Match(url).Groups;

        if (!TryGetValue(groups, "ownerHttps", "ownerSSH", out var owner) || !TryGetValue(groups, "projectHttps", "projectSSH", out var project))
        {
            throw new UriFormatException($"The url '{url}' is not a valid GitHub url, the Owner and or Project are not present.");
        }

        repositorySettings = new RepositorySettings(owner, project, headBranchName);
    }

    private static bool TryGetValue(GroupCollection groups, string groupName1, string groupName2, out string value)
    {
        value = groups[groupName1].Value;
        if (!string.IsNullOrEmpty(value))
        {
            return true;
        }

        value = groups[groupName2].Value;
        if (!string.IsNullOrEmpty(value))
        {
            return true;
        }

        return false;
    }
}