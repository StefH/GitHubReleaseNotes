using System;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Logic.Models;

internal class ReleaseInfo
{
    public long Version { get; set; }

    public string FriendlyName { get; set; } = null!;

    public DateTimeOffset When { get; set; }

    public IReadOnlyList<IssueInfo> IssueInfos { get; set; } = new List<IssueInfo>();
}