using System;
using System.Threading.Tasks;

namespace GitHubReleaseNotes.Logic
{
    public static class Generator
    {
        public static async Task GenerateAsync(string path)
        {
            var releaseInfos = await RepositoryHelper.GetReleaseInfoAsync(path);

            string result = HandleBarsHelper.Generate(releaseInfos);

            Console.WriteLine(result);
        }
    }
}