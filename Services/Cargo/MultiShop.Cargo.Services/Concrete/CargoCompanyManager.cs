using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoCompanyDtos;

namespace MultiShop.Cargo.Services.Concrete;

public class CargoCompanyManager(IUnitOfWork unitOfWork) : ICargoCompanyService
{
    private readonly IEfCargoCompanyRepository cargoCompanyRepository = unitOfWork.CargoCompanyRepository;
    
    public async Task<IReadOnlyList<ResultCargoCompanyDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cargoCompanyRepository.GetAll()
            .Select(x => new ResultCargoCompanyDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                CompanyEmail = x.CompanyEmail,
                CompanyPhoneNumber = x.CompanyPhoneNumber,
                CompanyAddress = x.CompanyAddress,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetByIdCargoCompanyDto?> GetByIdAsync(int id)
    {
        var entity = await cargoCompanyRepository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new GetByIdCargoCompanyDto
        {
            Id = entity.Id,
            CompanyName = entity.CompanyName,
            CompanyEmail = entity.CompanyEmail,
            CompanyPhoneNumber = entity.CompanyPhoneNumber,
            CompanyAddress = entity.CompanyAddress,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCargoCompanyDto dto)
    {
        var now = DateTime.UtcNow;
        var entity = new CargoCompany
        {
            CompanyName = dto.CompanyName,
            CompanyEmail = dto.CompanyEmail,
            CompanyPhoneNumber = dto.CompanyPhoneNumber,
            CompanyAddress = dto.CompanyAddress,
            CreatedAt = now,
            UpdatedAt = now,
            IsActive = true,
            IsDeleted = false
        };

        await cargoCompanyRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateCargoCompanyDto dto)
    {
        var entity = await cargoCompanyRepository.GetByIdAsync(dto.Id);
        if (entity is null)
            return false;

        entity.CompanyName = dto.CompanyName;
        entity.CompanyEmail = dto.CompanyEmail;
        entity.CompanyPhoneNumber = dto.CompanyPhoneNumber;
        entity.CompanyAddress = dto.CompanyAddress;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        cargoCompanyRepository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await cargoCompanyRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync();
    }
}
