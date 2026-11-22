using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

namespace MultiShop.Cargo.Services.Concrete;

public class CargoOperationManager(IUnitOfWork unitOfWork) : ICargoOperationService
{
    private readonly IEfCargoOperationRepository cargoOperationRepository = unitOfWork.CargoOperationRepository;
    
    public async Task<IReadOnlyList<ResultCargoOperationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cargoOperationRepository.GetAll()
            .Select(x => new ResultCargoOperationDto
            {
                Id = x.Id,
                Location = x.Location,
                TargetLocation = x.TargetLocation,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetByIdCargoOperationDto?> GetByIdAsync(int id)
    {
        var entity = await cargoOperationRepository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new GetByIdCargoOperationDto
        {
            Id = entity.Id,
            Location = entity.Location,
            TargetLocation = entity.TargetLocation,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCargoOperationDto dto)
    {
        var now = DateTime.UtcNow;
        var entity = new CargoOperation
        {
            Location = dto.Location,
            TargetLocation = dto.TargetLocation,
            CreatedAt = now,
            UpdatedAt = now,
            IsActive = true,
            IsDeleted = false
        };

        await cargoOperationRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateCargoOperationDto dto)
    {
        var entity = await cargoOperationRepository.GetByIdAsync(dto.Id);
        if (entity is null)
            return false;

        entity.Location = dto.Location;
        entity.TargetLocation = dto.TargetLocation;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        cargoOperationRepository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await cargoOperationRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync();
    }
}
