using System;

namespace GitHubReleaseNotes.Logic.Models;

internal class IssueInfo
{
    public int Number { get; set; }

    [Obsolete("Use IsPullRequest")]
    public bool IsPulRequest => IsPullRequest;

    public bool IsPullRequest { get; set; }

    public string IssueUrl { get; set; }

    public string Title { get; set; }

    public string User { get; set; }

    public string UserUrl { get; set; }

    public string[] Labels { get; set; }
}