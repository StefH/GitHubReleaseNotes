using System.IO;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static Task MainAsync(string[] args)
        {
            var parser = new SimpleCommandLineParser();
            parser.Parse(args);

            string repositoryPath = Path.Combine(parser.GetStringValue("path", string.Empty), ".git");
            string outputFile = parser.GetStringValue("output");

            return Generator.GenerateAsync(repositoryPath, outputFile);
        }
    }
}