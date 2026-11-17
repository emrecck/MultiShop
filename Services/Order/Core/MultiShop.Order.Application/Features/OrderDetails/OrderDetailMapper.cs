using MultiShop.Order.Application.Features.OrderDetails.Commands.Create;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Update;
using MultiShop.Order.Application.Features.OrderDetails.Queries.Get;
using MultiShop.Order.Application.Features.OrderDetails.Queries.GetById;
using MultiShop.Order.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Order.Application.Features.OrderDetails;

[Mapper]
public partial class OrderDetailMapper
{
    [MapperIgnoreTarget(nameof(OrderDetail.Id))]
    public partial OrderDetail ToEntity(CreateOrderDetailCommand source);
    public partial OrderDetail ToEntity(UpdateOrderDetailCommand source);
    public partial GetOrderDetailByIdQueryResult ToGetByIdDto(OrderDetail source);
    public partial IEnumerable<GetOrderDetailQueryResult> ToGetResultDto(IEnumerable<OrderDetail> source);
}
