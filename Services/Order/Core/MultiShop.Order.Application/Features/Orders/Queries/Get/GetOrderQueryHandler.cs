using MediatR;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Queries.Get;

public class GetOrderQueryHandler(IRepository<OrderEntity> repository, OrderMapper mapper) : IRequestHandler<GetOrderQuery, IEnumerable<GetOrderQueryResult>>
{
    public async Task<IEnumerable<GetOrderQueryResult>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var orders = await repository.GetAllAsync(cancellationToken);
        return mapper.ToGetResultDto(orders);
    }
}
