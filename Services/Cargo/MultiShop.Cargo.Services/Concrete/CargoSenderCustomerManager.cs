using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoSenderCustomerDtos;

namespace MultiShop.Cargo.Services.Concrete;

public class CargoSenderCustomerManager(IUnitOfWork unitOfWork)
    : ICargoSenderCustomerService
{
    private readonly IEfCargoSenderCustomerRepository cargoSenderCustomerRepository = unitOfWork.CargoSenderCustomerRepository;
    
    public async Task<IReadOnlyList<ResultCargoSenderCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cargoSenderCustomerRepository.GetAll()
            .Select(x => new ResultCargoSenderCustomerDto
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

    public async Task<GetByIdCargoSenderCustomerDto?> GetByIdAsync(int id)
    {
        var entity = await cargoSenderCustomerRepository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new GetByIdCargoSenderCustomerDto
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

    public async Task<int> CreateAsync(CreateCargoSenderCustomerDto dto)
    {
        var now = DateTime.UtcNow;
        var entity = new CargoSenderCustomer
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

        await cargoSenderCustomerRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateCargoSenderCustomerDto dto)
    {
        var entity = await cargoSenderCustomerRepository.GetByIdAsync(dto.Id);
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

        cargoSenderCustomerRepository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await cargoSenderCustomerRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync();
    }
}
