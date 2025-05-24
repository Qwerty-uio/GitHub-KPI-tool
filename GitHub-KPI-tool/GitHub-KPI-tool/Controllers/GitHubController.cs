using GitHub_KPI_tool_Application.Features.GitHub.GetGitHubMetricsPerPerson;
using GitHub_KPI_tool_Application.Features.GitHub.GetRepository;
using GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi")]
[ApiController]
public class GitHubController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public GitHubController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// This endpoint returns summary report of person's GitHub activity
    /// </summary>
    /// <param name="id">The id which belongs to person</param>
    [HttpGet]
    [Route("/github-kpi")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetGitHubMetricsByPersonQuery());
        
        return Ok(result);
    }
    
    
    
    
}