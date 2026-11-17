using MultiShop.Order.Application.Features.Addresses.Commands.Create;
using MultiShop.Order.Application.Features.Addresses.Commands.Update;
using MultiShop.Order.Application.Features.Addresses.Queries.Get;
using MultiShop.Order.Application.Features.Addresses.Queries.GetById;
using MultiShop.Order.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Order.Application.Features.Addresses
{
    [Mapper]
    public partial class AddressMapper
    {
        [MapperIgnoreTarget(nameof(Address.Id))]
        public partial Address ToEntity(CreateAddressCommand source);
        public partial Address ToEntity(UpdateAddressCommand source);
        public partial GetAddressByIdQueryResult ToGetByIdDto(Address source);
        public partial IEnumerable<GetAddressQueryResult> ToGetResultDto(IEnumerable<Address> source);
    }
}
