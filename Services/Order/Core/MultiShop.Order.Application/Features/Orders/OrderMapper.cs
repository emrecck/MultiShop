using MultiShop.Order.Application.Features.Orders.Commands.Create;
using MultiShop.Order.Application.Features.Orders.Commands.Update;
using MultiShop.Order.Application.Features.Orders.Queries.Get;
using MultiShop.Order.Application.Features.Orders.Queries.GetById;
using MultiShop.Order.Domain.Entities;
using Riok.Mapperly.Abstractions;
using OrderEntity = MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Orders;

[Mapper]
public partial class OrderMapper
{
    [MapperIgnoreTarget(nameof(Ordering.Id))]
    [MapperIgnoreTarget(nameof(Ordering.OrderDetails))]
    public partial Ordering ToEntity(CreateOrderCommand source);

    [MapperIgnoreTarget(nameof(Ordering.OrderDetails))]
    public partial Ordering ToEntity(UpdateOrderCommand source);

    [MapperIgnoreSource(nameof(Ordering.OrderDetails))]
    public partial GetOrderByIdQueryResult ToGetByIdDto(Ordering source);

    public partial IEnumerable<GetOrderQueryResult> ToGetResultDto(IEnumerable<Ordering> source);

    [MapperIgnoreSource(nameof(Ordering.OrderDetails))]
    private partial GetOrderQueryResult ToGetResultDto(Ordering source);
}
