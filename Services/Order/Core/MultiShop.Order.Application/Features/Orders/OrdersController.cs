using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Orders.Commands.Create;
using MultiShop.Order.Application.Features.Orders.Commands.Delete;
using MultiShop.Order.Application.Features.Orders.Commands.Update;
using MultiShop.Order.Application.Features.Orders.Queries.Get;
using MultiShop.Order.Application.Features.Orders.Queries.GetByCustomerId;
using MultiShop.Order.Application.Features.Orders.Queries.GetById;

namespace MultiShop.Order.Application.Features.Orders;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all orders")]
    [ProducesResponseType(typeof(IEnumerable<GetOrderQueryResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetOrderQueryResult>>> GetAll(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetOrderQuery(), cancellationToken));

    [HttpGet("by-customer/{customerId:int}")]
    [EndpointSummary("Get orders by customer ID")]
    [ProducesResponseType(typeof(IEnumerable<GetOrderQueryResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetOrderQueryResult>>> GetByCustomerId(int customerId, CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetOrdersByCustomerIdQuery(customerId), cancellationToken));

    [HttpGet("{id:int}")]
    [EndpointSummary("Get an order by ID")]
    [ProducesResponseType(typeof(GetOrderByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetOrderByIdQueryResult>> GetById(int id, CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetOrderByIdQuery(id), cancellationToken));

    [HttpPost]
    [EndpointSummary("Create an order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update an order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Route ID and body ID must match.");

        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete an order")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteOrderCommand(id), cancellationToken);
        return NoContent();
    }
}
