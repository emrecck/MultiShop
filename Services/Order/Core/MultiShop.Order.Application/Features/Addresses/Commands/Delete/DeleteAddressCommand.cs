using MediatR;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Delete;

public record DeleteAddressCommand(int Id) : IRequest;