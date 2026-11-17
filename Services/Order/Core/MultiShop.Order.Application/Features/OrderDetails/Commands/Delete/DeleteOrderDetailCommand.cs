using MediatR;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Delete;

public record DeleteOrderDetailCommand(int Id) : IRequest;
