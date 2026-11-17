using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using OrderEntity = MultiShop.Order.Domain.Entities.Ordering;

namespace MultiShop.Order.Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommandHandler(IRepository<OrderEntity> repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException("Order not found");
        }

        await repository.DeleteAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
