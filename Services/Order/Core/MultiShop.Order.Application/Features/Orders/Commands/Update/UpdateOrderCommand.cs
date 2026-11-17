using MediatR;

namespace MultiShop.Order.Application.Features.Orders.Commands.Update;

public record UpdateOrderCommand(
    int Id,
    int CustomerId,
    decimal TotalPrice,
    DateTimeOffset OrderDate
) : IRequest;
