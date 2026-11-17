namespace MultiShop.Order.Application.Features.OrderDetails.Queries.Get;

public record GetOrderDetailQueryResult(
    int Id,
    int OrderId,
    int ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    decimal UnitPrice
);
