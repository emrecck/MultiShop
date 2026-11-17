using System;
using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Update;

public class UpdateAddressCommandHandler(IRepository<Address> repository, AddressMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAddressCommand>
{

    public async Task Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (address is null)
        {
            throw new EntityNotFoundException("Address not found");
        }

        repository.Update(mapper.ToEntity(command));
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
