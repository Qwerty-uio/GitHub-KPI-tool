using GitHub_KPI_tool_Application.Abstraction.ApiClients;
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
        var issues = await _getIssues.Get(request.Owner, request.Repository);
        var result = new GetIssuesResult()
        {
            Issues = issues
        };
        
        return result;
    }
}