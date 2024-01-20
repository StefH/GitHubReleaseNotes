using System;
using System.Reflection;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic;

namespace GitHubReleaseNotes;

static class Program
{
    static async Task Main(string[] args)
    {
        await MainAsync(args).ConfigureAwait(false);
    }

    private static Task MainAsync(string[] args)
    {
        var configuration = ConfigurationParser.Parse(args);

        Console.WriteLine($"GitHubReleaseNotes ({Assembly.GetExecutingAssembly().GetName().Version})");
        return new Generator(configuration).GenerateAsync();
    }
}