using System.IO;
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
                Language = parser.GetStringValue("language", "en"),
                Version = parser.GetStringValue("version", "next")
            };
        }
    }
}