using MediatR;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Create;

public record CreateAddressCommand(
    int CustomerId,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
) : IRequest<int>;