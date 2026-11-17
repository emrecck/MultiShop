using MediatR;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Update;

public record UpdateOrderDetailCommand(
    int Id,
    int OrderId,
    int ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    decimal UnitPrice
) : IRequest;
