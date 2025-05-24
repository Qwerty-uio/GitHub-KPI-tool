namespace GitHub_KPI_tool_Application.Models.Commit
{
    public class GitHubCommitModel
    {
        public GitHubCommitModel(string author, string commit, string commitMessage, Stats stats)
        {
            Author = author;
            Commit = commit;
            CommitMessage = commitMessage;
            Stats = stats;
        }

        public string? Author { get; set; }

        public string Commit { get; set; }

        public string CommitMessage { get; set; }
        
        public Stats Stats { get; set; }
    }
}
