using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;
using MediatR;
using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetCommits;

public class GetCommitsHandler : IRequestHandler<GetCommitsQuery, GetCommitsResult>
{
    private readonly IGetCommits _getCommits;
    public GetCommitsHandler( IGetCommits getCommits)
    {
        _getCommits = getCommits;
    }

    public async Task<GetCommitsResult> Handle(GetCommitsQuery request, CancellationToken cancellationToken)
    {
        List<GitHubCommitModel> commits;
        if (request.DateFrom is null || request.DateTo is null)
        {
            commits = await _getCommits.Get(request.Owner, request.Repository);
        }
        else
        {
            commits= await _getCommits.GetByDate(request.Owner, request.Repository, request.DateFrom, request.DateTo);
        }

        var result = new GetCommitsResult()
        {
            Commits = commits
        };
        
        return result;
    }
    
}