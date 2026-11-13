using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices;

public interface IProductImageService
{
    Task<IEnumerable<ResultProductImageDto>> GetAllProductImagesAsync(CancellationToken cancellationToken);
    Task<GetByIdProductImageDto?> GetProductImageByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateProductImageAsync(CreateProductImageDto productImage, CancellationToken cancellationToken);
    Task<bool> UpdateProductImageAsync(UpdateProductImageDto productImage, CancellationToken cancellationToken);
    Task<bool> DeleteProductImageAsync(string id, CancellationToken cancellationToken);
}
