using System;
using System.Collections.Generic;

namespace GitHubReleaseNotes.Models
{
    internal class ReleaseInfo
    {
        public long? Version { get; set; }

        public string FriendlyName { get; set; }

        public DateTimeOffset When { get; set; }

        public List<IssueInfo> IssueInfos { get; set; }
    }
}