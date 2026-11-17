using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Addresses.Commands.Create;
using MultiShop.Order.Application.Features.Addresses.Commands.Delete;
using MultiShop.Order.Application.Features.Addresses.Commands.Update;
using MultiShop.Order.Application.Features.Addresses.Queries.Get;
using MultiShop.Order.Application.Features.Addresses.Queries.GetById;

namespace MultiShop.Order.Application.Features.Addresses;

[Route("api/[controller]")]
[ApiController]
public class AdressesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all addresses")]
    [ProducesResponseType(typeof(IEnumerable<GetAddressQueryResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetAddressQueryResult>>> GetAll(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetAddressQuery(), cancellationToken));

    [HttpGet("{id:int}")]
    [EndpointSummary("Get an address by ID")]
    [ProducesResponseType(typeof(GetAddressByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAddressByIdQueryResult>> GetById(int id, CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetAddressByIdQuery(id), cancellationToken));

    [HttpPost]
    [EndpointSummary("Create an address")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update an address")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Route ID and body ID must match.");

        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete an address")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteAddressCommand(id), cancellationToken);
        return NoContent();
    }
}
