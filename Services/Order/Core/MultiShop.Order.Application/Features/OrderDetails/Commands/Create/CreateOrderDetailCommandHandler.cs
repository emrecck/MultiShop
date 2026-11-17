using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Create;

public class CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository, OrderDetailMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderDetailCommand, int>
{
    public async Task<int> Handle(CreateOrderDetailCommand command, CancellationToken cancellationToken)
    {
        var orderDetail = mapper.ToEntity(command);
        await repository.AddAsync(orderDetail, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return orderDetail.Id;
    }
}
