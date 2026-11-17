using MediatR;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetById;

public record GetOrderDetailByIdQuery(int Id) : IRequest<GetOrderDetailByIdQueryResult>;
