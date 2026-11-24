using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;
using Microsoft.Extensions.Options;

namespace MultiShop.Catalog.Services.ProductDetailServices;

public class ProductDetailService : IProductDetailService
{
    private readonly IMongoDatabase _mongodb;
    private readonly IMongoCollection<ProductDetail> _productDetails;
    private readonly ProductDetailMapper _mapper;

    public ProductDetailService(ProductDetailMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var databaseSettings = databaseSettingsOptions.Value;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        _mongodb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _productDetails = _mongodb.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);
    }

    public async Task<string> CreateProductDetailAsync(
        CreateProductDetailDto productDetail,
        CancellationToken cancellationToken)
    {
        var newProductDetail = _mapper.ToEntity(productDetail);
        await _productDetails.InsertOneAsync(newProductDetail, cancellationToken: cancellationToken);
        return newProductDetail.Id;
    }

    public async Task<bool> DeleteProductDetailAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _productDetails.DeleteOneAsync(
            productDetail => productDetail.Id == id,
            cancellationToken: cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultProductDetailDto>> GetAllProductDetailsAsync(CancellationToken cancellationToken)
    {
        var productDetails = await _productDetails.Find(_ => true).ToListAsync(cancellationToken);
        return productDetails.Select(productDetail => _mapper.ToResultDto(productDetail));
    }

    public async Task<GetByIdProductDetailDto?> GetProductDetailByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var productDetail = await _productDetails.Find(item => item.Id == id).FirstOrDefaultAsync(cancellationToken);
        return productDetail is null ? null : _mapper.ToGetByIdDto(productDetail);
    }

    public async Task<GetByIdProductDetailDto?> GetProductDetailByProductIdAsync(
        string productId,
        CancellationToken cancellationToken)
    {
        var productDetail = await _productDetails.Find(item => item.ProductId == productId).FirstOrDefaultAsync(cancellationToken);
        return productDetail is null ? null : _mapper.ToGetByIdDto(productDetail);
    }

    public async Task<bool> UpdateProductDetailAsync(
        UpdateProductDetailDto productDetail,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ProductDetail>.Filter.Eq(item => item.Id, productDetail.ProductDetailId);
        var update = Builders<ProductDetail>.Update
            .Set(item => item.Description, productDetail.ProductDetailDescription)
            .Set(item => item.Information, productDetail.ProductDetailInfo)
            .Set(item => item.ProductId, productDetail.ProductId);
        var result = await _productDetails.UpdateOneAsync(
            filter,
            update,
            cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
