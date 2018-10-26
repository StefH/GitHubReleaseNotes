using System;
using System.Threading.Tasks;

namespace GitHubReleaseNotes
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var releaseInfos = await RepositoryHelper.GetReleaseInfoAsync(@"C:\Users\\StefHeyenrath\Documents\Github\WireMock.Net\.git");

            foreach (var releaseInfo in releaseInfos)
            {
                Console.WriteLine(releaseInfo.FriendlyName);
                Console.WriteLine(releaseInfo.When);
                foreach (var issueInfo in releaseInfo.IssueInfos)
                {
                    Console.WriteLine("  " + issueInfo.ToString());
                }
                Console.WriteLine("");
            }
        }
    }
}