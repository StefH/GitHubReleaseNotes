using Octokit;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Logic.Models
{
    internal class IssuesAndPullRequestsModel
    {
        public ICollection<Issue> Issues { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }
    }
}