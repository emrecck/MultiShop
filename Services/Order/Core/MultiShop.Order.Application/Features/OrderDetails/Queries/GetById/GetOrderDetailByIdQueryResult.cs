namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetById;

public record GetOrderDetailByIdQueryResult(
    int Id,
    int OrderId,
    int ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    decimal UnitPrice
);
