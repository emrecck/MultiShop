using MediatR;

namespace MultiShop.Order.Application.Features.Orders.Commands.Create;

public record CreateOrderCommand(
    int CustomerId,
    decimal TotalPrice,
    DateTimeOffset OrderDate
) : IRequest<int>;
