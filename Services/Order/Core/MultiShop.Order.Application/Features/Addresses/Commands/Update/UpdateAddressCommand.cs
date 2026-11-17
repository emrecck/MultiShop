using MediatR;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Update;

public record UpdateAddressCommand(
    int Id,
    int CustomerId,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
) : IRequest;