using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoDetailDtos;

namespace MultiShop.Cargo.Services.Concrete;

public class CargoDetailManager(IUnitOfWork unitOfWork) : ICargoDetailService
{
    private readonly IEfCargoDetailRepository cargoDetailRepository = unitOfWork.CargoDetailRepository;
    
    public async Task<IReadOnlyList<ResultCargoDetailDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cargoDetailRepository.GetAll()
            .Select(x => new ResultCargoDetailDto
            {
                Id = x.Id,
                CargoCustomerId = x.CargoCustomerId,
                CargoCompanyId = x.CargoCompanyId,
                TrackingNumber = x.TrackingNumber,
                Description = x.Description,
                EstimatedDeliveryDate = x.EstimatedDeliveryDate,
                DeliveredAt = x.DeliveredAt,
                IsDelivered = x.IsDelivered,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetByIdCargoDetailDto?> GetByIdAsync(int id)
    {
        var entity = await cargoDetailRepository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new GetByIdCargoDetailDto
        {
            Id = entity.Id,
            CargoCustomerId = entity.CargoCustomerId,
            CargoCompanyId = entity.CargoCompanyId,
            TrackingNumber = entity.TrackingNumber,
            Description = entity.Description,
            EstimatedDeliveryDate = entity.EstimatedDeliveryDate,
            DeliveredAt = entity.DeliveredAt,
            IsDelivered = entity.IsDelivered,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCargoDetailDto dto)
    {
        var now = DateTime.UtcNow;
        var entity = new CargoDetail
        {
            CargoCustomerId = dto.CargoCustomerId,
            CargoCompanyId = dto.CargoCompanyId,
            TrackingNumber = dto.TrackingNumber,
            Description = dto.Description,
            EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
            DeliveredAt = dto.DeliveredAt,
            IsDelivered = dto.IsDelivered,
            CreatedAt = now,
            UpdatedAt = now,
            IsActive = true,
            IsDeleted = false
        };

        await cargoDetailRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateCargoDetailDto dto)
    {
        var entity = await cargoDetailRepository.GetByIdAsync(dto.Id);
        if (entity is null)
            return false;

        entity.CargoCustomerId = dto.CargoCustomerId;
        entity.CargoCompanyId = dto.CargoCompanyId;
        entity.TrackingNumber = dto.TrackingNumber;
        entity.Description = dto.Description;
        entity.EstimatedDeliveryDate = dto.EstimatedDeliveryDate;
        entity.DeliveredAt = dto.DeliveredAt;
        entity.IsDelivered = dto.IsDelivered;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        cargoDetailRepository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await cargoDetailRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync();
    }
}
