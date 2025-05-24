using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.Calculator.Issue;

public class GetMarksForIssuesHandler: IRequestHandler<GetMarksForIssuesQuery, GetMarksForIssuesResult>
{
    private readonly IIssueCalculator _issueCalculator;
    private readonly IGetIssues _getIssues;

    public GetMarksForIssuesHandler(IIssueCalculator issueCalculator, IGetIssues getIssues)
    {
        _issueCalculator = issueCalculator;
        _getIssues = getIssues;
    }

    public async Task<GetMarksForIssuesResult> Handle(GetMarksForIssuesQuery request, CancellationToken cancellationToken)
    {
        var response = _issueCalculator.CalculateMarkForIssues(await _getIssues.GetByDate(request.Owner,request.Repository,request.DateFrom, cancellationToken));
        var result = new GetMarksForIssuesResult()
        {
            MarksForIssues = response
        };
        
        return result;
    }
}