using MediatR;

namespace MultiShop.Order.Application.Features.Orders.Queries.GetById;

public record GetOrderByIdQuery(int Id) : IRequest<GetOrderByIdQueryResult>;
