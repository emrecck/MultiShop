using MediatR;

namespace MultiShop.Order.Application.Features.Addresses.Queries.GetById;

public record GetAddressByIdQuery(int Id) : IRequest<GetAddressByIdQueryResult>;