using System;
using System.IO;
using System.Threading.Tasks;

namespace GitHubReleaseNotes.Logic
{
    public static class Generator
    {
        public static async Task GenerateAsync(string repositoryPath, string outputFile = null)
        {
            var releaseInfos = await RepositoryHelper.GetReleaseInfoAsync(repositoryPath);

            string result = HandleBarsHelper.Generate(releaseInfos);

            if (!string.IsNullOrEmpty(outputFile))
            {
                Console.WriteLine($"Writing Release Notes to '{new FileInfo(outputFile).FullName}'");
                File.WriteAllText(outputFile, result);
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}