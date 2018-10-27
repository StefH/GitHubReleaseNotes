using System;
using System.Collections.Generic;
using GitHubReleaseNotes.Logic.Models;
using HandlebarsDotNet;

namespace GitHubReleaseNotes.Logic
{
    internal static class HandleBarsHelper
    {
        private static string templateText =
            "{{#each releaseInfos}}# {{ FriendlyName }} ({{formatDate When \"dd MMMM yyyy\"}})\r\n{{#each issueInfos}}- {{Text}}\r\n{{/each}}\r\n\r\n{{/each}}";

        static HandleBarsHelper()
        {
            Handlebars.RegisterHelper("formatDate", (writer, context, arguments) =>
            {
                switch (arguments[0])
                {
                    case DateTimeOffset value:
                        writer.WriteSafeString(value.ToString(arguments[1] as string));
                        break;

                    case DateTime value:
                        writer.WriteSafeString(value.ToString(arguments[1] as string));
                        break;
                }
            });
        }

        internal static string Generate(IEnumerable<ReleaseInfo> releaseInfos)
        {
            var template = Handlebars.Compile(templateText);

            return template(new { releaseInfos });
        }
    }
}
