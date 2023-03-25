using Octokit;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Logic.Models;

internal class IssuesAndPullRequestsModel
{
    public IReadOnlyList<Issue> Issues { get; set; } = new List<Issue>();

    public IReadOnlyList<PullRequest> PullRequests { get; set; } = new List<PullRequest>();
}