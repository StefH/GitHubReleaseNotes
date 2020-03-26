using System;
using Octokit;

namespace GitHubReleaseNotes.Logic
{
    internal static class GitHubClientFactory
    {
        private const string AppName = "GitHubReleaseNotes";

        public static IGitHubClient CreateClient(Configuration configuration, string owner)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string product = !string.IsNullOrEmpty(owner) ? owner : AppName;
            var client = new GitHubClient(new ProductHeaderValue(product));

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
    }
}