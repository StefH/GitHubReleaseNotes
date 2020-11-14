using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using GitHubReleaseNotes.Logic;

namespace CoverageConverter
{
    public class Program
    {
        internal class Options : IConfiguration
        {
            /*
              
                SkipEmptyReleases = parser.GetBoolValue("skip-empty-releases") || parser.Contains("skip-empty-releases"), // "--skip-empty-releases true" and "--skip-empty-releases" both qualify
            */

            [Option('p', "path", Required = true, Default = "")]
            public string Path { get; set; }

            public string RepositoryPath => System.IO.Path.Combine(Path, ".git");

            [Option('o', "output", Required = true, Default = "")]
            public string OutputFile { get; set; }

            [Option('v', "version", Required = true, Default = "next")]
            public string Version { get; set; }

            [Option('l', "language", Required = true, Default = "en")]
            public string Language { get; set; }

            public CultureInfo CultureInfo => string.IsNullOrEmpty(Language) || Language == "system" ? CultureInfo.CurrentCulture : new CultureInfo(Language);

            [Option('s', "skip-empty-releases")]
            public bool SkipEmptyReleases { get; set; }

            [Option('t', "template")]
            public string TemplatePath { get; set; }

            [Option("login")]
            public string Login { get; set; }

            [Option("password")]
            public string Password { get; set; }

            [Option("token")]
            public string Token { get; set; }

            [Option('l', "exclude-labels")]
            public IEnumerable<string> ExcludeLabels { get; set; }
        }

        static async Task Main(string[] args)
        {
            await new Parser(cfg =>
            {
                cfg.AutoVersion = false;
                cfg.AutoHelp = true;
                cfg.HelpWriter = Console.Out;
            }).ParseArguments<Options>(args).WithParsedAsync(options => new Generator(options).GenerateAsync());
        }
    }
}