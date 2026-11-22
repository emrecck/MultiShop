using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoReceiverCustomerDtos;

namespace MultiShop.Cargo.Services.Concrete;

public class CargoReceiverCustomerManager(IUnitOfWork unitOfWork)
    : ICargoReceiverCustomerService
{
    private readonly IEfCargoReceiverCustomerRepository cargoReceiverCustomerRepository = unitOfWork.CargoReceiverCustomerRepository;
    
    public async Task<IReadOnlyList<ResultCargoReceiverCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cargoReceiverCustomerRepository.GetAll()
            .Select(x => new ResultCargoReceiverCustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                District = x.District,
                City = x.City,
                Address = x.Address,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetByIdCargoReceiverCustomerDto?> GetByIdAsync(int id)
    {
        var entity = await cargoReceiverCustomerRepository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new GetByIdCargoReceiverCustomerDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            District = entity.District,
            City = entity.City,
            Address = entity.Address,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCargoReceiverCustomerDto dto)
    {
        var now = DateTime.UtcNow;
        var entity = new CargoReceiverCustomer
        {
            Name = dto.Name,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            District = dto.District,
            City = dto.City,
            Address = dto.Address,
            CreatedAt = now,
            UpdatedAt = now,
            IsActive = true,
            IsDeleted = false
        };

        await cargoReceiverCustomerRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateCargoReceiverCustomerDto dto)
    {
        var entity = await cargoReceiverCustomerRepository.GetByIdAsync(dto.Id);
        if (entity is null)
            return false;

        entity.Name = dto.Name;
        entity.Email = dto.Email;
        entity.PhoneNumber = dto.PhoneNumber;
        entity.District = dto.District;
        entity.City = dto.City;
        entity.Address = dto.Address;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        cargoReceiverCustomerRepository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await cargoReceiverCustomerRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync();
    }
}
