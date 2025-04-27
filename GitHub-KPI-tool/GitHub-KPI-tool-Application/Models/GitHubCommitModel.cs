namespace GitHub_KPI_tool_Application.Models
{
    public class GitHubCommitModel
    {
        public GitHubCommitModel(string author, string commit, string commitMessage)
        {
            Author = author;
            Commit = commit;
            CommitMessage = commitMessage;
        }

        public string Author { get; set; }

        public string Commit { get; set; }

        public string CommitMessage { get; set; }
    }
}
