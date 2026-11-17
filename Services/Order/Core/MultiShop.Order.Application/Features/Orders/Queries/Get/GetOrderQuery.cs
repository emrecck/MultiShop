using MediatR;

namespace MultiShop.Order.Application.Features.Orders.Queries.Get;

public record GetOrderQuery() : IRequest<IEnumerable<GetOrderQueryResult>>;
