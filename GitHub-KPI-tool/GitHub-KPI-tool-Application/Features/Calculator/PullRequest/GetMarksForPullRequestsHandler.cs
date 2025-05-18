using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.Calculator.PullRequest;

public class GetMarksForPullRequestsHandler : IRequestHandler<GetMarksForPullRequestsQuery, GetMarksForPullRequestsResult>
{
    private readonly IPullRequestCalculator _pullRequestCalculator;
    private readonly IGetPullRequests _getPullRequests;

    public GetMarksForPullRequestsHandler(IPullRequestCalculator pullRequestCalculator, IGetPullRequests getPullRequests)
    {
        _pullRequestCalculator = pullRequestCalculator;
        _getPullRequests = getPullRequests;
    }

    public async Task<GetMarksForPullRequestsResult> Handle(GetMarksForPullRequestsQuery request, CancellationToken cancellationToken)
    {
        var response = _pullRequestCalculator.CalculateMarkForPullRequests(await _getPullRequests.GetByDate(request.Owner, request.Repository, request.DateFrom));
        var result = new GetMarksForPullRequestsResult()
        {
            MarksForPullRequests = response
        };
        
        return result;
    }
}