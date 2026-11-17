using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Create;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Delete;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Update;
using MultiShop.Order.Application.Features.OrderDetails.Queries.Get;
using MultiShop.Order.Application.Features.OrderDetails.Queries.GetById;

namespace MultiShop.Order.Application.Features.OrderDetails;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all order details")]
    [ProducesResponseType(typeof(IEnumerable<GetOrderDetailQueryResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetOrderDetailQueryResult>>> GetAll(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetOrderDetailQuery(), cancellationToken));

    [HttpGet("{id:int}")]
    [EndpointSummary("Get an order detail by ID")]
    [ProducesResponseType(typeof(GetOrderDetailByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetOrderDetailByIdQueryResult>> GetById(int id, CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetOrderDetailByIdQuery(id), cancellationToken));

    [HttpPost]
    [EndpointSummary("Create an order detail")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDetailCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update an order detail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDetailCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Route ID and body ID must match.");

        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete an order detail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteOrderDetailCommand(id), cancellationToken);
        return NoContent();
    }
}
