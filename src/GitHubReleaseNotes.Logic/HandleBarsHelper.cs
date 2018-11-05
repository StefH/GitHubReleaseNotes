using System;
using System.Collections.Generic;
using GitHubReleaseNotes.Logic.Models;
using HandlebarsDotNet;

namespace GitHubReleaseNotes.Logic
{
    internal class HandleBarsHelper
    {
        private const string PartialHeader = "# {{ FriendlyName }} ({{formatDate When \"dd MMMM yyyy\"}})\r\n";
        private const string PartialIssueLabels = "{{#if Labels}} [{{join Labels \", \"}}]{{/if}}";
        private const string PartialIssueContributed = "{{#if IsPulRequest}} contributed by [{{User}}]({{UserUrl}}){{/if}}";
        private const string PartialIssueText = "- [#{{Number}}]({{IssueUrl}}) - {{Title}}{{> IssueLabels}}{{> IssueContributed}}";
        private const string PartialIssues = "{{#each issueInfos}}{{> IssueText}}\r\n{{/each}}\r\n\r\n";
        private const string TemplateText = "{{#each releaseInfos}}{{> Header}}{{> Issues}}{{/each}}";

        private readonly Configuration _configuration;

        public HandleBarsHelper(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        internal string Generate(IEnumerable<ReleaseInfo> releaseInfos)
        {
            RegisterTemplates();
            RegisterHelpers();

            var template = Handlebars.Compile(TemplateText);
            return template(new { releaseInfos });
        }

        private void RegisterTemplates()
        {
            Handlebars.RegisterTemplate("Header", PartialHeader);
            Handlebars.RegisterTemplate("Issues", PartialIssues);
            Handlebars.RegisterTemplate("IssueText", PartialIssueText);
            Handlebars.RegisterTemplate("IssueLabels", PartialIssueLabels);
            Handlebars.RegisterTemplate("IssueContributed", PartialIssueContributed);
        }

        private void RegisterHelpers()
        {
            Handlebars.RegisterHelper("join", (writer, context, arguments) =>
            {
                if (arguments[0] is IEnumerable<object> enumerable)
                {
                    string concatenatedString = string.Join(arguments[1] as string, enumerable);
                    writer.WriteSafeString(concatenatedString);
                }
            });

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
