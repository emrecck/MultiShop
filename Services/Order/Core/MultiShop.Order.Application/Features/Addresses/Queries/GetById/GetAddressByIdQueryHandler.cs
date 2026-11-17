using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MediatR;
using MultiShop.Order.Application.Exceptions;

namespace MultiShop.Order.Application.Features.Addresses.Queries.GetById
{
    public class GetAddressByIdQueryHandler(IRepository<Address> repository, AddressMapper mapper) : IRequestHandler<GetAddressByIdQuery, GetAddressByIdQueryResult>
    {
        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            var address = await repository.GetByIdAsync(query.Id, cancellationToken);
            if (address == null)
            {
                throw new EntityNotFoundException($"Address with ID {query.Id} not found.");
            }

            return mapper.ToGetByIdDto(address);
        }
    }
}
