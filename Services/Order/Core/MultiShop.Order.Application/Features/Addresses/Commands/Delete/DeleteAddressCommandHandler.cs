using System;
using MediatR;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Delete;

public class DeleteAddressCommandHandler(IRepository<Address> repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAddressCommand>
{
    public async Task Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (address is null)
        {
            throw new EntityNotFoundException("Address not found");
        }

        await repository.DeleteAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
