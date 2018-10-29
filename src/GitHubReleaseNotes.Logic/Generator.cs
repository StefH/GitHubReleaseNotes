using System;
using System.IO;
using System.Threading.Tasks;

namespace GitHubReleaseNotes.Logic
{
    public static class Generator
    {
        public static async Task GenerateAsync(Configuration configuration)
        {
            var releaseInfos = await RepositoryHelper.GetReleaseInfoAsync(configuration);

            string result = HandleBarsHelper.Generate(releaseInfos);

            if (!string.IsNullOrEmpty(configuration.OutputFile))
            {
                Console.WriteLine($"Writing Release Notes to '{new FileInfo(configuration.OutputFile).FullName}'");
                File.WriteAllText(configuration.OutputFile, result);
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}