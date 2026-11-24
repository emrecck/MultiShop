using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices;

public class StatisticService : IStatisticService
{
    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<Category> _categories;
    private readonly IMongoCollection<Brand> _brands;

    public StatisticService(IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _products = db.GetCollection<Product>(settings.ProductCollectionName);
        _categories = db.GetCollection<Category>(settings.CategoryCollectionName);
        _brands = db.GetCollection<Brand>(settings.BrandCollectionName);
    }

    public Task<long> GetBrandCountAsync(CancellationToken cancellationToken) =>
        _brands.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public Task<long> GetCategoryCountAsync(CancellationToken cancellationToken) =>
        _categories.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public Task<long> GetProductCountAsync(CancellationToken cancellationToken) =>
        _products.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public async Task<decimal> GetProductAvgPriceAsync(CancellationToken cancellationToken)
    {
        var prices = await _products.Find(_ => true).Project(x => x.Price).ToListAsync(cancellationToken);
        return prices.Count == 0 ? 0m : prices.Average();
    }

    public async Task<string> GetMaxPriceProductNameAsync(CancellationToken cancellationToken)
    {
        var product = await _products.Find(_ => true).SortByDescending(x => x.Price).FirstOrDefaultAsync(cancellationToken);
        return product?.Name ?? string.Empty;
    }

    public async Task<string> GetMinPriceProductNameAsync(CancellationToken cancellationToken)
    {
        var product = await _products.Find(_ => true).SortBy(x => x.Price).FirstOrDefaultAsync(cancellationToken);
        return product?.Name ?? string.Empty;
    }
}
