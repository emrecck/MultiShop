using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetById;

public class GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository, OrderDetailMapper mapper) : IRequestHandler<GetOrderDetailByIdQuery, GetOrderDetailByIdQueryResult>
{
    public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query, CancellationToken cancellationToken)
    {
        var orderDetail = await repository.GetByIdAsync(query.Id, cancellationToken);
        if (orderDetail is null)
        {
            throw new EntityNotFoundException($"Order detail with ID {query.Id} not found.");
        }

        return mapper.ToGetByIdDto(orderDetail);
    }
}
