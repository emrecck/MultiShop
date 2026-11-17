using MediatR;

namespace MultiShop.Order.Application.Features.Addresses.Queries.Get;

public record GetAddressQuery() : IRequest<IEnumerable<GetAddressQueryResult>>;