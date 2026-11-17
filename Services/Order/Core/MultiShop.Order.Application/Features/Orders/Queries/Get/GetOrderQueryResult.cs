namespace MultiShop.Order.Application.Features.Orders.Queries.Get;

public record GetOrderQueryResult(
    int Id,
    int CustomerId,
    decimal TotalPrice,
    DateTimeOffset OrderDate
);
