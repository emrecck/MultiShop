namespace MultiShop.Order.Application.Features.Orders.Queries.GetById;

public record GetOrderByIdQueryResult(
    int Id,
    int CustomerId,
    decimal TotalPrice,
    DateTimeOffset OrderDate
);
