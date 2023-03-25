using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes;

public class Program
{
    internal class Options : IConfiguration
    {
        [Option("path", Default = "", HelpText = "The path from the git repository. If not supplied, the current folder is used.")]
        public string Path { get; set; } = string.Empty;

        public string RepositoryPath => System.IO.Path.Combine(Path, ".git");

        [Option("output", HelpText = "The location from the generated Release Notes. If not supplied, the output is written to the console.")]
        public string? OutputFile { get; set; }

        [Option("version", Default = "next", HelpText = "Define a custom version name for the latest release instead of the value 'next'.")]
        public string Version { get; set; } = "next";

        [Option("language", Default = "en", HelpText = "Provide the language (two letter according to ISO-639-1) which is used to format the dates. If not provided, 'en' is used. It's also possible to use a value like 'system', which takes the current system ui language.")]
        public string Language { get; set; } = "en";

        public CultureInfo Culture => string.IsNullOrEmpty(Language) || Language == "system" ? CultureInfo.CurrentCulture : new CultureInfo(Language);

        [Option("skip-empty-releases", HelpText = "Define this optional argument to skip writing releases which have no associated Issues or Pull Requests.")]
        public bool SkipEmptyReleases { get; set; }

        [Option("template", HelpText = "Provide a custom Handlebars template instead of the default template to generate the Release Notes.")]
        public string? TemplatePath { get; set; }

        [Option("token", HelpText = "Provide the GitHub API token as authentication for connecting to private repositories. Or to get more GitHub API requests.")]
        public string? Token { get; set; }

        [Option("login", HelpText = "Provide the GitHub API login as authentication for connecting to private repositories.")]
        public string? Login { get; set; }

        [Option("password", HelpText = "Provide the GitHub API password as authentication for connecting to private repositories.")]
        public string? Password { get; set; }

        [Option("exclude-labels", HelpText = "Exclude Issues and Pull Requests which have these labels set.")]
        public IEnumerable<string>? ExcludeLabels { get; set; }
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine($"GitHubReleaseNotes ({Assembly.GetExecutingAssembly().GetName().Version})");

        await new Parser(cfg =>
        {
            cfg.AutoVersion = false;
            cfg.AutoHelp = true;
            cfg.HelpWriter = Console.Out;
        }).ParseArguments<Options>(args).WithParsedAsync(options => new Generator(options).GenerateAsync());
    }
}