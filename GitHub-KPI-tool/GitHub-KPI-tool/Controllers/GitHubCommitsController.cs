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
    public async Task<ActionResult> Get(string owner, string repository)
    {
        var result = await _mediator.Send(new GetCommitsQuery(owner, repository));
        
        return Ok(result);
    }
}