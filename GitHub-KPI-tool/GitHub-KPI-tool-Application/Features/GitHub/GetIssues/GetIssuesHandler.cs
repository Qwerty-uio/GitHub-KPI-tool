using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Models.Issue;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetIssues;

public class GetIssuesHandler:IRequestHandler<GetIssuesQuery,GetIssuesResult>
{
    private readonly IGetIssues _getIssues;

    public GetIssuesHandler(IGetIssues getIssues)
    {
        _getIssues = getIssues;
    }

    public async Task<GetIssuesResult> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
    {
        List<GitHubIssueModel> issues;
        if (request.DateFrom is null)
        {
            issues = await _getIssues.Get(request.Owner, request.Repository, cancellationToken);
        }
        else
        {
            issues = await _getIssues.GetByDate(request.Owner, request.Repository, request.DateFrom, cancellationToken);
        }
        var result = new GetIssuesResult()
        {
            Issues = issues
        };
        
        return result;
    }
}