using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices;

public interface ICategoryService
{
    Task<IEnumerable<ResultCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<GetByIdCategoryDto?> GetCategoryByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateCategoryAsync(CreateCategoryDto category, CancellationToken cancellationToken);
    Task<bool> UpdateCategoryAsync(UpdateCategoryDto category, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(string id, CancellationToken cancellationToken);
}