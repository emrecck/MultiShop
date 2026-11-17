namespace MultiShop.Order.Application.Features.Addresses.Queries.Get;

public record GetAddressQueryResult(
    int Id,
    int CustomerId,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
);