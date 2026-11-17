using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler(IRepository<OrderEntity> repository, OrderMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException("Order not found");
        }

        repository.Update(mapper.ToEntity(command));
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
