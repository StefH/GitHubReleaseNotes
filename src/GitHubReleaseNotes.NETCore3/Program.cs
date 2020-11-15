using System;
using System.Reflection;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = ConfigurationParser.Parse(args);

            Console.WriteLine($"GitHubReleaseNotes ({Assembly.GetExecutingAssembly().GetName().Version})");
            await new Generator(configuration).GenerateAsync();
        }
    }
}