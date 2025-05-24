using GitHub_KPI_tool_Application.Features.Calculator.Commit;
using GitHub_KPI_tool_Application.Features.GitHub.GetCommits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi/commits")]
[ApiController]
public class GitHubCommitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubCommitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/github-kpi/commits")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCommits(string owner, string repository)
    {
        var result = await _mediator.Send(new GetCommitsQuery(owner, repository));
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("/github-kpi/commits/date")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCommitsByDate(string owner, string repository, DateTimeOffset dateFrom, DateTimeOffset dateTo)
    {
        var result = await _mediator.Send(new GetCommitsQuery(owner, repository)
        {
            DateFrom = dateFrom,
            DateTo = dateTo
        });
        
        return Ok(result);
    }

    [HttpGet]
    [Route("/github-kpi/commits/marks")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCommitsMarks(string owner, string repository, DateTimeOffset dateFrom, DateTimeOffset dateTo)
    {
        var result = await _mediator.Send(new GetMarksForCommitsQuery(owner, repository, dateFrom, dateTo));
        return Ok(result);
    }
}