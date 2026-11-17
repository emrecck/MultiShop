using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Create;

public class CreateAddressCommandHandler(IRepository<Address> repository, AddressMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, int>
{
    public async Task<int> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var address = mapper.ToEntity(command);
        await repository.AddAsync(address, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return address.Id;
    }
}