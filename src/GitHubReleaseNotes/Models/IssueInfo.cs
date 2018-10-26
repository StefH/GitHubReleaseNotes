namespace GitHubReleaseNotes.Models
{
    internal class IssueInfo
    {
        public int Id { get; set; }

        public bool IsPulRequest { get; set; }

        public string IssueUrl { get; set; }

        public string Title { get; set; }

        public string User { get; set; }

        public string UserUrl { get; set; }

        public override string ToString()
        {
            string extra = IsPulRequest ? $" PR contributed by [{User}]({UserUrl})" : "";

            return $"- [#{Id}]({IssueUrl}) - {Title}{extra}";
        }
    }
}