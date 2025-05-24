using MediatR;

namespace GitHub_KPI_tool_Application.Features.Calculator.Commit;

public record GetMarksForCommitsQuery(string Owner, string Repository, DateTimeOffset DateFrom, DateTimeOffset DateTo):IRequest<GetMarksForCommitsResult>;