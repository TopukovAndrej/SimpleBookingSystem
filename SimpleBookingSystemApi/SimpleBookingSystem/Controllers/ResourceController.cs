using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Application.Commands.Resource;
using SimpleBookingSystem.Application.Queries.Resource;
using SimpleBookingSystem.Contracts.Dtos.Resource;
using SimpleBookingSystem.Contracts.Models;
using SimpleBookingSystem.Contracts.Requests.Resource;

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

    [HttpPost]
    [Route("book-resource")]
    public async Task<IActionResult> BookResourceAsync([FromBody] BookResourceRequest request)
    {
        Result commandResult = await mediator.Send(request: new BookResourceCommand(request: request));

        if (commandResult.IsSuccess)
        {
            return Ok();
        }
        else
        {
            if (commandResult.ErrorMessage == "Failed to fetch existing bookings for the resource!")
            {
                return StatusCode(statusCode: 500, value: commandResult.ErrorMessage);
            }
            else if (commandResult.ErrorMessage!.Contains("not found!"))
            {
                return NotFound(value: commandResult.ErrorMessage);
            }
            else
            {
                return BadRequest(error: commandResult.ErrorMessage);
            }
        }
    }
}
