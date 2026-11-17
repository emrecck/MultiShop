using MediatR;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler(IRepository<OrderEntity> repository, OrderMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = mapper.ToEntity(command);
        await repository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
