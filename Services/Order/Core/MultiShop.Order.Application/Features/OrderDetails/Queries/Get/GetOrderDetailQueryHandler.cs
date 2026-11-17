using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.Get;

public class GetOrderDetailQueryHandler(IRepository<OrderDetail> repository, OrderDetailMapper mapper) : IRequestHandler<GetOrderDetailQuery, IEnumerable<GetOrderDetailQueryResult>>
{
    public async Task<IEnumerable<GetOrderDetailQueryResult>> Handle(GetOrderDetailQuery query, CancellationToken cancellationToken)
    {
        var orderDetails = await repository.GetAllAsync(cancellationToken);
        return mapper.ToGetResultDto(orderDetails);
    }
}
