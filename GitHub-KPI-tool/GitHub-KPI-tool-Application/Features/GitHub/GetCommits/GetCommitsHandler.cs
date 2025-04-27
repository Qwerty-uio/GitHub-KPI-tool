using GitHub_KPI_tool_Application.Abstraction.ApiClients;
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
        var commits = await _getCommits.Get(request.Owner, request.Repository);
        var result = new GetCommitsResult()
        {
            Commits = commits
        };
        
        return result;
    }
    
}