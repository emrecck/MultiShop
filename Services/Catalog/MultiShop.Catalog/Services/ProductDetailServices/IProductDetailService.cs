using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices;

public interface IProductDetailService
{
    Task<IEnumerable<ResultProductDetailDto>> GetAllProductDetailsAsync(CancellationToken cancellationToken);
    Task<GetByIdProductDetailDto?> GetProductDetailByIdAsync(string id, CancellationToken cancellationToken);
    Task<GetByIdProductDetailDto?> GetProductDetailByProductIdAsync(string productId, CancellationToken cancellationToken);
    Task<string> CreateProductDetailAsync(CreateProductDetailDto productDetail, CancellationToken cancellationToken);
    Task<bool> UpdateProductDetailAsync(UpdateProductDetailDto productDetail, CancellationToken cancellationToken);
    Task<bool> DeleteProductDetailAsync(string id, CancellationToken cancellationToken);
}
