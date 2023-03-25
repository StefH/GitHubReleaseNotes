using System;

namespace GitHubReleaseNotes.Logic.Models;

internal class IssueInfo
{
    public int Number { get; set; }

    [Obsolete("Use IsPullRequest")]
    public bool IsPulRequest => IsPullRequest;

    public bool IsPullRequest { get; set; }

    public string IssueUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string User { get; set; } = null!;

    public string UserUrl { get; set; } = null!;

    public string[] Labels { get; set; } = null!;
}