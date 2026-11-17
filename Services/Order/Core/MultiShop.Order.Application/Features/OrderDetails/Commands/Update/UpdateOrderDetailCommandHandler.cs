using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Update;

public class UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository, OrderDetailMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderDetailCommand>
{
    public async Task Handle(UpdateOrderDetailCommand command, CancellationToken cancellationToken)
    {
        var orderDetail = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (orderDetail is null)
        {
            throw new EntityNotFoundException("Order detail not found");
        }

        repository.Update(mapper.ToEntity(command));
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
