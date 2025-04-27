using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using MediatR;
using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;

public class GetPullRequestHandler : IRequestHandler<GetPullRequestQuery, GetPullRequestResult>
{
    private readonly IGetPullRequests _getPullRequests;

    public GetPullRequestHandler(IGetPullRequests getPullRequests)
    {
        _getPullRequests = getPullRequests;
    }

    public async Task<GetPullRequestResult> Handle(GetPullRequestQuery request, CancellationToken cancellationToken)
    {
        var pullRequests = await _getPullRequests.Get(request.Owner, request.Repository);
        var result = new GetPullRequestResult()
        {
            PullRequests = pullRequests
        };
        
        return result;
    }
}