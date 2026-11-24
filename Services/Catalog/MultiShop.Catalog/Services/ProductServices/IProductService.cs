using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices;

public interface IProductService
{
    Task<IEnumerable<ResultProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ResultProductDto>> GetProductsByCategoryIdAsync(string categoryId, CancellationToken cancellationToken);
    Task<GetByIdProductDto?> GetProductByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateProductAsync(CreateProductDto product, CancellationToken cancellationToken);
    Task<bool> UpdateProductAsync(UpdateProductDto product, CancellationToken cancellationToken);
    Task<bool> DeleteProductAsync(string id, CancellationToken cancellationToken);
}
