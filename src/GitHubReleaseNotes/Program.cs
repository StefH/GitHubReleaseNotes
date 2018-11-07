using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes
{
    static class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static Task MainAsync(string[] args)
        {
            var configuration = ParseConfiguration(args);

            Console.WriteLine($"GitHubReleaseNotes ({Assembly.GetExecutingAssembly().GetName().Version})");
            return new Generator(configuration).GenerateAsync();
        }

        private static Configuration ParseConfiguration(string[] args)
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
                SkipEmptyReleases = parser.GetBoolValue("skip-empty-releases") || parser.Contains("skip-empty-releases") // "--skip-empty-releases true" and "--skip-empty-releases" both qualify
            };
        }
    }
}