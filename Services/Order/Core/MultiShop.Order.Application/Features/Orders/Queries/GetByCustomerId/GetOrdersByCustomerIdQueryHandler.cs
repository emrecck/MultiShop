using MediatR;
using MultiShop.Order.Application.Features.Orders.Queries.Get;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Queries.GetByCustomerId;

public class GetOrdersByCustomerIdQueryHandler(IRepository<OrderEntity> repository, OrderMapper mapper)
    : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<GetOrderQueryResult>>
{
    public async Task<IEnumerable<GetOrderQueryResult>> Handle(
        GetOrdersByCustomerIdQuery query,
        CancellationToken cancellationToken)
    {
        var orders = await repository.GetAllAsync(cancellationToken);
        var filtered = orders.Where(x => x.CustomerId == query.CustomerId);
        return mapper.ToGetResultDto(filtered);
    }
}
