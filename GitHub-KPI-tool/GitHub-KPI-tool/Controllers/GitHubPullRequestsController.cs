using GitHub_KPI_tool_Application.Features.Calculator.PullRequest;
using GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi/pull-requests")]
[ApiController]
public class GitHubPullRequestsController: ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubPullRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("/github-kpi/pull-requests")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPullRequests(string owner, string repository)
    {
        var result = await _mediator.Send(new GetPullRequestQuery(owner, repository));
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("/github-kpi/pull-requests/date")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPullRequestsByDate(string owner, string repository, DateTimeOffset dateFrom, DateTimeOffset dateTo)
    {
        var result = await _mediator.Send(new GetPullRequestQuery(owner, repository)
        {
            DateFrom = dateFrom
        });
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("/github-kpi/pull-requests/marks")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPullRequestsMarks(string owner, string repository, DateTimeOffset dateFrom)
    {
        var result = await _mediator.Send(new GetMarksForPullRequestsQuery(owner, repository, dateFrom));
        
        return Ok(result);
    }
}