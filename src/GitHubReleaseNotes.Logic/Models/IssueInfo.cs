namespace GitHubReleaseNotes.Logic.Models;

internal class IssueInfo
{
    public required int Number { get; set; }

    public required bool IsPulRequest { get; set; }

    public required string IssueUrl { get; set; }

    public required string Title { get; set; }

    public required string User { get; set; }

    public required string UserUrl { get; set; }

    public required string[] Labels { get; set; }
}