using System.Globalization;

namespace GitHubReleaseNotes.Logic
{
    public class Configuration
    {
        public string RepositoryPath { get; set; }

        public string OutputFile { get; set; }

        public string Version { get; set; }

        public CultureInfo Culture { get; set; }

        public bool SkipEmptyReleases { get; set; }
    }
}