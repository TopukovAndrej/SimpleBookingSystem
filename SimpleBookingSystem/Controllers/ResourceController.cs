using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Application.Queries.Resource;
using SimpleBookingSystem.Contracts.Dtos.Resource;
using SimpleBookingSystem.Contracts.Models;

namespace SimpleBookingSystem.API.Controllers;

[ApiController]
[Route("/api/resources")]
public class ResourceController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllResourcesAsync()
    {
        Result<IReadOnlyList<ResourceDto>> queryResult = await mediator.Send(request: new GetAllResourcesQuery());

        if (queryResult.IsSuccess)
        {
            return Ok(value: queryResult.Value);
        }
        else
        {
            return NotFound(value: queryResult.ErrorMessage);
        }
    }
}
