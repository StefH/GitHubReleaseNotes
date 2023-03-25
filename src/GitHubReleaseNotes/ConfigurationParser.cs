using System.IO;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes;

internal static class ConfigurationParser
{
    internal static Configuration Parse(string[] args)
    {
        var parser = new SimpleCommandLineParser();
        parser.Parse(args);

        return new Configuration
        {
            RepositoryPath = Path.Combine(parser.GetStringValue("path", string.Empty), ".git"),
            OutputFile = parser.GetStringValue("output"),
            Culture = parser.GetCultureInfo("language"),
            Version = parser.GetStringValue("version", "next"),
            TemplatePath = parser.GetStringValue("template"),
            SkipEmptyReleases = parser.GetBoolValue("skip-empty-releases") || parser.Contains("skip-empty-releases"), // "--skip-empty-releases true" and "--skip-empty-releases" both qualify
            Token = parser.GetStringValue("token"),
            Login = parser.GetStringValue("login"),
            Password = parser.GetStringValue("password"),
            ExcludeLabels = parser.GetValues("exclude-labels")
        };
    }
}