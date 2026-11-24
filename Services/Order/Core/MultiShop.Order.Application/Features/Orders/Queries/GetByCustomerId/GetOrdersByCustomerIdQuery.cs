using MediatR;
using MultiShop.Order.Application.Features.Orders.Queries.Get;

namespace MultiShop.Order.Application.Features.Orders.Queries.GetByCustomerId;

public record GetOrdersByCustomerIdQuery(int CustomerId) : IRequest<IEnumerable<GetOrderQueryResult>>;
