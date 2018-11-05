using System.Collections.Generic;
using System.Linq;

namespace GitHubReleaseNotes.Logic.Models
{
    internal class IssueInfo
    {
        public int Number { get; set; }

        public bool IsPulRequest { get; set; }

        public string IssueUrl { get; set; }

        public string Title { get; set; }

        public string User { get; set; }

        public string UserUrl { get; set; }

        public IEnumerable<string> Labels { get; set; }

        public string Text
        {
            get
            {
                string labels = Labels != null && Labels.Any() ? $" [{string.Join(", ", Labels)}]" : string.Empty;
                string extra = IsPulRequest ? $" contributed by [{User}]({UserUrl})" : "";

                return $"[#{Number}]({IssueUrl}) - {Title}{labels}{extra}";

            }
        }
    }
}