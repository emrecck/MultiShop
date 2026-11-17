using MediatR;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.Get;

public record GetOrderDetailQuery() : IRequest<IEnumerable<GetOrderDetailQueryResult>>;
