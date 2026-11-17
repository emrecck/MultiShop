using MediatR;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Create;

public record CreateOrderDetailCommand(
    int OrderId,
    int ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    decimal UnitPrice
) : IRequest<int>;
