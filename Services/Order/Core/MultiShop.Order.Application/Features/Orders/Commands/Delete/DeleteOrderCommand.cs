using MediatR;

namespace MultiShop.Order.Application.Features.Orders.Commands.Delete;

public record DeleteOrderCommand(int Id) : IRequest;
