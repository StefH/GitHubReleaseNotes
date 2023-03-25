using System.Collections.Generic;
using System.Globalization;

namespace GitHubReleaseNotes.Logic;

public interface IConfiguration
{
    string RepositoryPath { get; }

    string? OutputFile { get; }

    string Version { get; }

    CultureInfo Culture { get; }

    bool SkipEmptyReleases { get; }

    string? TemplatePath { get; }

    string? Login { get; }

    string? Password { get; }

    string? Token { get; }

    IEnumerable<string>? ExcludeLabels { get; }
}