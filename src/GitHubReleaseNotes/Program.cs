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
            return Generator.GenerateAsync(@"C:\Users\\StefHeyenrath\Documents\Github\WireMock.Net\.git");
        }
    }
}