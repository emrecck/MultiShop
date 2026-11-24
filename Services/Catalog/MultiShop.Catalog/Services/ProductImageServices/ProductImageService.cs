using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;
using Microsoft.Extensions.Options;

namespace MultiShop.Catalog.Services.ProductImageServices;

public class ProductImageService : IProductImageService
{
    private readonly IMongoDatabase _mongodb;
    private readonly IMongoCollection<ProductImage> _productImages;
    private readonly ProductImageMapper _mapper;

    public ProductImageService(ProductImageMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var databaseSettings = databaseSettingsOptions.Value;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        _mongodb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _productImages = _mongodb.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
    }

    public async Task<string> CreateProductImageAsync(
        CreateProductImageDto productImage,
        CancellationToken cancellationToken)
    {
        var newProductImage = _mapper.ToEntity(productImage);
        await _productImages.InsertOneAsync(newProductImage, cancellationToken: cancellationToken);
        return newProductImage.Id;
    }

    public async Task<bool> DeleteProductImageAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _productImages.DeleteOneAsync(
            productImage => productImage.Id == id,
            cancellationToken: cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultProductImageDto>> GetAllProductImagesAsync(CancellationToken cancellationToken)
    {
        var productImages = await _productImages.Find(_ => true).ToListAsync(cancellationToken);
        return productImages.Select(productImage => _mapper.ToResultDto(productImage));
    }

    public async Task<GetByIdProductImageDto?> GetProductImageByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var productImage = await _productImages.Find(item => item.Id == id).FirstOrDefaultAsync(cancellationToken);
        return productImage is null ? null : _mapper.ToGetByIdDto(productImage);
    }

    public async Task<GetByIdProductImageDto?> GetProductImageByProductIdAsync(
        string productId,
        CancellationToken cancellationToken)
    {
        var productImage = await _productImages.Find(item => item.ProductId == productId).FirstOrDefaultAsync(cancellationToken);
        return productImage is null ? null : _mapper.ToGetByIdDto(productImage);
    }

    public async Task<bool> UpdateProductImageAsync(
        UpdateProductImageDto productImage,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ProductImage>.Filter.Eq(item => item.Id, productImage.Id);
        var update = Builders<ProductImage>.Update
            .Set(item => item.Image1, productImage.Image1)
            .Set(item => item.Image2, productImage.Image2)
            .Set(item => item.Image3, productImage.Image3)
            .Set(item => item.ProductId, productImage.ProductId);
        var result = await _productImages.UpdateOneAsync(
            filter,
            update,
            cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
