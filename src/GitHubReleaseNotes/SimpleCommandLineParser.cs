using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GitHubReleaseNotes
{
    // Based on
    // - http://blog.gauffin.org/2014/12/simple-command-line-parser/
    // - https://github.com/WireMock-Net/WireMock.Net/blob/master/src/WireMock.Net.StandAlone/SimpleCommandLineParser.cs
    internal class SimpleCommandLineParser
    {
        private const string Sigil = "--";

        private IDictionary<string, string[]> Arguments { get; } = new Dictionary<string, string[]>();

        public void Parse(string[] args)
        {
            string currentName = null;

            var values = new List<string>();
            foreach (string arg in args)
            {
                if (arg.StartsWith(Sigil))
                {
                    if (!string.IsNullOrEmpty(currentName))
                    {
                        Arguments[currentName] = values.ToArray();
                    }

                    values.Clear();
                    currentName = arg.Substring(Sigil.Length);
                }
                else if (string.IsNullOrEmpty(currentName))
                {
                    Arguments[arg] = new string[0];
                }
                else
                {
                    values.Add(arg);
                }
            }

            if (!string.IsNullOrEmpty(currentName))
            {
                Arguments[currentName] = values.ToArray();
            }
        }

        public bool Contains(string name)
        {
            return Arguments.ContainsKey(name);
        }

        public string[] GetValues(string name, string[] defaultValue = null)
        {
            return Contains(name) ? Arguments[name] : defaultValue;
        }

        public T GetValue<T>(string name, Func<string[], T> func, T defaultValue = default(T))
        {
            return Contains(name) ? func(Arguments[name]) : defaultValue;
        }

        public bool GetBoolValue(string name, bool defaultValue = false)
        {
            return GetValue(name, values =>
            {
                string value = values.FirstOrDefault();
                return !string.IsNullOrEmpty(value) ? bool.Parse(value) : defaultValue;
            }, defaultValue);
        }

        public int? GetIntValue(string name, int? defaultValue = null)
        {
            return GetValue(name, values =>
            {
                string value = values.FirstOrDefault();
                return !string.IsNullOrEmpty(value) ? int.Parse(value) : defaultValue;
            }, defaultValue);
        }

        public string GetStringValue(string name, string defaultValue = null)
        {
            return GetValue(name, values => values.FirstOrDefault() ?? defaultValue, defaultValue);
        }

        public CultureInfo GetCultureInfo(string name)
        {
            return GetValue(name, values =>
            {
                string value = values.FirstOrDefault() ?? "en";

                return value == "system" ? CultureInfo.CurrentCulture : new CultureInfo(value);
            });
        }
    }
}