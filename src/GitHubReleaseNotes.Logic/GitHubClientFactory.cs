using System;
using Octokit;

namespace GitHubReleaseNotes.Logic
{
    internal static class GitHubClientFactory
    {
        private const string AppName = "GitHubReleaseNotes";

        public static IGitHubClient CreateClient(Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var client = CreateClient();

            if (!string.IsNullOrEmpty(configuration.Token))
            {
                client.Credentials = new Credentials(configuration.Token);
            }
            else if (!string.IsNullOrEmpty(configuration.Login) && !string.IsNullOrEmpty(configuration.Password))
            {
                client.Credentials = new Credentials(configuration.Login, configuration.Password);
            }

            return client;
        }

        private static GitHubClient CreateClient()
        {
            return new GitHubClient(new ProductHeaderValue(AppName));
        }
    }
}