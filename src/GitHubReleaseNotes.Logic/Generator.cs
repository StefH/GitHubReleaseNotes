using System;
using System.Threading.Tasks;

namespace GitHubReleaseNotes.Logic
{
    public static class Generator
    {
        public static async Task GenerateAsync(string path)
        {
            var releaseInfos = await RepositoryHelper.GetReleaseInfoAsync(path);

            foreach (var releaseInfo in releaseInfos)
            {
                Console.WriteLine(releaseInfo.FriendlyName);
                Console.WriteLine(releaseInfo.When);
                foreach (var issueInfo in releaseInfo.IssueInfos)
                {
                    Console.WriteLine("  " + issueInfo);
                }
                Console.WriteLine("");
            }
        }
    }
}