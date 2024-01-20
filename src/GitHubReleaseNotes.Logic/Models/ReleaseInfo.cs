using System;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Logic.Models;

internal class ReleaseInfo
{
    public required long Version { get; set; }

    public required string FriendlyName { get; set; }

    public required DateTimeOffset When { get; set; }

    public List<IssueInfo> IssueInfos { get; set; } = null!;
}