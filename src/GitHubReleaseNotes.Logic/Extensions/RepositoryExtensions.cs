using System;
using System.Collections.Generic;
using System.Linq;
using GitHubReleaseNotes.Logic.Models;
using LibGit2Sharp;

namespace GitHubReleaseNotes.Logic.Extensions;

internal static class RepositoryExtensions
{
    public static IReadOnlyList<ReleaseInfo> GetOrderedReleaseInfos(this Repository repo, string version)
    {
        var orderedReleaseInfos = repo.Tags

            // Convert Tag into ReleaseInfo
            .Select(tag => new ReleaseInfo
            {
                Version = GetVersionAsLong(tag.FriendlyName) ?? 0,
                FriendlyName = tag.FriendlyName,
                When = tag.Target is Commit commit ? commit.Committer.When : DateTimeOffset.MinValue
            })

            // Skip invalid versions
            .Where(tag => tag.Version > 0)

            // Order by the version
            .OrderBy(tag => tag.Version)
            .ToList();

        // Add the `next` version
        orderedReleaseInfos.Add(new ReleaseInfo
        {
            Version = long.MaxValue,
            FriendlyName = version,
            When = DateTimeOffset.Now
        });

        return orderedReleaseInfos;
    }

    private static long? GetVersionAsLong(string friendlyName)
    {
        var versionAsString = new string(friendlyName.Where(c => char.IsDigit(c) || c == '.').ToArray());
        if (System.Version.TryParse(versionAsString, out var version))
        {
            return version.Major * 1000000000L + version.Minor * 1000000L + (version.Build > 0 ? version.Build : 0) * 1000L + (version.Revision > 0 ? version.Revision : 0);
        }

        return null;
    }
}