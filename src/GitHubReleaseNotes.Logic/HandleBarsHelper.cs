using System;
using System.Collections.Generic;
using GitHubReleaseNotes.Logic.Models;
using HandlebarsDotNet;

namespace GitHubReleaseNotes.Logic
{
    internal class HandleBarsHelper
    {
        private const string TemplateText =
            "{{#each releaseInfos}}# {{ FriendlyName }} ({{formatDate When \"dd MMMM yyyy\"}})\r\n{{#each issueInfos}}- {{Text}}\r\n{{/each}}\r\n\r\n{{/each}}";

        private readonly Configuration _configuration;

        public HandleBarsHelper(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        internal string Generate(IEnumerable<ReleaseInfo> releaseInfos)
        {
            RegisterHelper();

            var template = Handlebars.Compile(TemplateText);

            return template(new { releaseInfos });
        }

        private void RegisterHelper()
        {
            Handlebars.RegisterHelper("formatDate", (writer, context, arguments) =>
            {
                switch (arguments[0])
                {
                    case DateTimeOffset value:
                        writer.WriteSafeString(value.ToString(arguments[1] as string, _configuration.Culture));
                        break;

                    case DateTime value:
                        writer.WriteSafeString(value.ToString(arguments[1] as string, _configuration.Culture));
                        break;
                }
            });
        }
    }
}
