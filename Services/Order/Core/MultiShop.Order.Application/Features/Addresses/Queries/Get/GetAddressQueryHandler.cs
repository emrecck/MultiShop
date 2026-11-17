using System;
using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Queries.Get;

public class GetAddressQueryHandler(IRepository<Address> repository, AddressMapper mapper) : IRequestHandler<GetAddressQuery, IEnumerable<GetAddressQueryResult>>
{
    public async Task<IEnumerable<GetAddressQueryResult>> Handle(GetAddressQuery query, CancellationToken cancellationToken)
    {
        var addresses = await repository.GetAllAsync(cancellationToken);
        return mapper.ToGetResultDto(addresses);
    }
}
