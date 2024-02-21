using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubReleaseNotes.Logic.Models;
using GitReader;
using GitReader.Primitive;
using GitReader.Structures;

namespace GitHubReleaseNotes.Logic.Extensions;

internal static class RepositoryExtensions
{
    public static async Task<IReadOnlyList<ReleaseInfo>> GetOrderedReleaseInfosAsync(this StructuredRepository repo, string version)
    {
        var orderedReleaseInfos = new List<ReleaseInfo>();

        var getCommitTasks = new Dictionary<string, Task<Commit?>>();

        foreach (var tag in repo.Tags)
        {
            var getCommitTask = repo.GetCommitAsync(tag.Value.ObjectHash);
            getCommitTasks.Add(tag.Key, getCommitTask);
        }

        await Task.WhenAll(getCommitTasks.Values);

        foreach (var tag in await Task.WhenAll(getCommitTasks.Values))
        {
            var tagVersion = GetVersionAsLong(tag.Key);
            if (tagVersion == null)
            {
                // Skip invalid versions
                continue;
            }

            var commit = tag.Value.Result;
            if (commit == null)
            {
                throw new InvalidOperationException($"Tag {tag.Key} has no commit.");
            }

            var releaseInfo = new ReleaseInfo
            {
                Version = tagVersion.Value,
                FriendlyName = tag.Key,
                When = commit.Committer.Date
            };
            orderedReleaseInfos.Add(releaseInfo);
        }

        orderedReleaseInfos = orderedReleaseInfos.OrderBy(tag => tag.Version).ToList();
        
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
        if (Version.TryParse(versionAsString, out var version))
        {
            return version.Major * 1000000000L + version.Minor * 1000000L + (version.Build > 0 ? version.Build : 0) * 1000L + (version.Revision > 0 ? version.Revision : 0);
        }

        return null;
    }
}