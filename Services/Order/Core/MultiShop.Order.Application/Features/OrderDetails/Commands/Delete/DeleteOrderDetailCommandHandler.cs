using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Delete;

public class DeleteOrderDetailCommandHandler(IRepository<OrderDetail> repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderDetailCommand>
{
    public async Task Handle(DeleteOrderDetailCommand command, CancellationToken cancellationToken)
    {
        var orderDetail = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (orderDetail is null)
        {
            throw new EntityNotFoundException("Order detail not found");
        }

        await repository.DeleteAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
