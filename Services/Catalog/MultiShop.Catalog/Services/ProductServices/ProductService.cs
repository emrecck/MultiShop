using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;
using Microsoft.Extensions.Options;

namespace MultiShop.Catalog.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IMongoDatabase _mongodb;
    private readonly IMongoCollection<Product> _products;
    private readonly ProductMapper _mapper;

    public ProductService(ProductMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var databaseSettings = databaseSettingsOptions.Value;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        _mongodb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _products = _mongodb.GetCollection<Product>(databaseSettings.ProductCollectionName);
    }

    public async Task<string> CreateProductAsync(
        CreateProductDto product,
        CancellationToken cancellationToken)
    {
        var newProduct = _mapper.ToEntity(product);
        await _products.InsertOneAsync(newProduct, cancellationToken: cancellationToken);
        return newProduct.Id;
    }

    public async Task<bool> DeleteProductAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _products.DeleteOneAsync(
            product => product.Id == id,
            cancellationToken: cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultProductDto>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _products.Find(_ => true).ToListAsync(cancellationToken);
        return products.Select(product => _mapper.ToResultDto(product));
    }

    public async Task<GetByIdProductDto?> GetProductByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var product = await _products.Find(item => item.Id == id).FirstOrDefaultAsync(cancellationToken);
        return product is null ? null : _mapper.ToGetByIdDto(product);
    }

    public async Task<bool> UpdateProductAsync(
        UpdateProductDto product,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(item => item.Id, product.ProductId);
        var update = Builders<Product>.Update
            .Set(item => item.Name, product.ProductName)
            .Set(item => item.Description, product.ProductDescription)
            .Set(item => item.Price, product.ProductPrice)
            .Set(item => item.ImageUrl, product.ProductImageUrl)
            .Set(item => item.Stock, product.ProductStock)
            .Set(item => item.CategoryId, product.CategoryId);
        var result = await _products.UpdateOneAsync(
            filter,
            update,
            cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
