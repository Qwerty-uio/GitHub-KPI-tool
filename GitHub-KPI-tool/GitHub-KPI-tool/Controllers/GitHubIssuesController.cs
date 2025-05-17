using GitHub_KPI_tool_Application.Features.GitHub.GetIssues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi/issues")]
public class GitHubIssuesController: ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubIssuesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/github-kpi/issues")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIssues(string owner, string repository)
    {
        var result = await _mediator.Send(new GetIssuesQuery(owner, repository));
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("/github-kpi/issues/date")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIssuesByDate(string owner, string repository, DateTimeOffset? dateFrom, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetIssuesQuery(owner, repository){
            DateFrom = dateFrom
        });
        
        return Ok(result);
    }
}