using System;
using System.IO;
using System.Threading.Tasks;

namespace GitHubReleaseNotes.Logic
{
    public class Generator
    {
        private readonly Configuration _configuration;
        private readonly RepositoryHelper _repositoryHelper;
        private readonly HandleBarsHelper _handleBarsHelper;

        public Generator(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _repositoryHelper = new RepositoryHelper(configuration);
            _handleBarsHelper = new HandleBarsHelper(configuration);
        }

        public async Task GenerateAsync()
        {
            var releaseInfos = await _repositoryHelper.GetReleaseInfoAsync();

            string result = _handleBarsHelper.Generate(releaseInfos);

            if (!string.IsNullOrEmpty(_configuration.OutputFile))
            {
                Console.WriteLine($"Release Notes written to '{new FileInfo(_configuration.OutputFile).FullName}'");
                File.WriteAllText(_configuration.OutputFile, result);
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}