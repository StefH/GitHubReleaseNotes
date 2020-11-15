using System;
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
            var configuration = ConfigurationParser.Parse(args);

            Console.WriteLine($"GitHubReleaseNotes ({Assembly.GetExecutingAssembly().GetName().Version})");
            return new Generator(configuration).GenerateAsync();
        }
    }
}