namespace MultiShop.Order.Application.Features.Addresses.Queries.GetById;

public record GetAddressByIdQueryResult(
    int Id,
    int CustomerId,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
);