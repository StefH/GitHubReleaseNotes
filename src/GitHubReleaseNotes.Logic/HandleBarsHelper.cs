using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GitHubReleaseNotes.Logic.Models;
using HandlebarsDotNet;

namespace GitHubReleaseNotes.Logic
{
    internal class HandleBarsHelper
    {
        private const string TemplateFilename = "GitHubReleaseNotes.Logic.Template.txt";

        private readonly IConfiguration _configuration;

        public HandleBarsHelper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        internal string Generate(IEnumerable<ReleaseInfo> releaseInfos)
        {
            RegisterHelpers();

            var template = Handlebars.Compile(GetTemplateAsString());

            return template(new { releaseInfos });
        }

        private string GetTemplateAsString()
        {
            // If provided, read custom Template
            if (!string.IsNullOrEmpty(_configuration.TemplatePath))
            {
                return File.ReadAllText(_configuration.TemplatePath);
            }

            // Use default embedded Template
            var assembly = typeof(HandleBarsHelper).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(TemplateFilename))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
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
                        writer.WriteSafeString(value.ToString(arguments[1] as string, _configuration.CultureInfo));
                        break;

                    case DateTime value:
                        writer.WriteSafeString(value.ToString(arguments[1] as string, _configuration.CultureInfo));
                        break;
                }
            });
        }
    }
}
