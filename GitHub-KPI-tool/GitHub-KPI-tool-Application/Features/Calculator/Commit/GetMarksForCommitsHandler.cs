using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.Calculator.Commit;

public class GetMarksForCommitsHandler: IRequestHandler<GetMarksForCommitsQuery, GetMarksForCommitsResult>
{
    private readonly ICommitCalculator _commitCalculator;
    private readonly IGetCommits _getCommits;

    public GetMarksForCommitsHandler(ICommitCalculator commitCalculator, IGetCommits getCommits)
    {
        _commitCalculator = commitCalculator;
        _getCommits = getCommits;
    }

    public async Task<GetMarksForCommitsResult> Handle(GetMarksForCommitsQuery request, CancellationToken cancellationToken)
    {
        var response = _commitCalculator.CalculateMarkForCommits(await _getCommits.GetByDate(request.Owner, request.Repository, request.DateFrom, request.DateTo));
        var result = new GetMarksForCommitsResult()
        {
            MarksForCommits = response
        };
        
        return result;
    }
}