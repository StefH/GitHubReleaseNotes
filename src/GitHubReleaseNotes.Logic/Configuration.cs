using System.Collections.Generic;
using System.Globalization;

namespace GitHubReleaseNotes.Logic;

public class Configuration : IConfiguration
{
    public string RepositoryPath { get; set; } = null!;

    public string? OutputFile { get; set; }

    public string? Version { get; set; }

    public CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;

    public bool SkipEmptyReleases { get; set; }

    public string? TemplatePath { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }

    public IEnumerable<string>? ExcludeLabels { get; set; }
}