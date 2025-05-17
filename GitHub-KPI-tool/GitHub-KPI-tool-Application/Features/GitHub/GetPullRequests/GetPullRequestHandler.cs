using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models.PullRequest;
using MediatR;
using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;

public class GetPullRequestHandler : IRequestHandler<GetPullRequestQuery, GetPullRequestResult>
{
    private readonly IGetPullRequests _getPullRequests;
    private readonly IPullRequestCalculator _pullRequestCalculator;

    public GetPullRequestHandler(IGetPullRequests getPullRequests, IPullRequestCalculator pullRequestCalculator)
    {
        _getPullRequests = getPullRequests;
        _pullRequestCalculator = pullRequestCalculator;
    }

    public async Task<GetPullRequestResult> Handle(GetPullRequestQuery request, CancellationToken cancellationToken)
    {
        List<GitHubPullRequestModel> pullRequests;
        if (request.DateFrom is null)
        {
            pullRequests = await _getPullRequests.Get(request.Owner, request.Repository);
        }
        else
        {
            pullRequests = await _getPullRequests.GetByDate(request.Owner, request.Repository, request.DateFrom, cancellationToken);
        }

        var result = new GetPullRequestResult()
        {
            PullRequests = pullRequests
        };
        
        return result;
    }
}