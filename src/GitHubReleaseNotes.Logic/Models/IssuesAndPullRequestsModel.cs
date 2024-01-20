using Octokit;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Logic.Models;

internal class IssuesAndPullRequestsModel
{
    public required ICollection<Issue> Issues { get; set; }

    public required ICollection<PullRequest> PullRequests { get; set; }
}