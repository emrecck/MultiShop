using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler(IRepository<OrderEntity> repository, OrderMapper mapper) : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResult>
{
    public async Task<GetOrderByIdQueryResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(query.Id, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException($"Order with ID {query.Id} not found.");
        }

        return mapper.ToGetByIdDto(order);
    }
}
