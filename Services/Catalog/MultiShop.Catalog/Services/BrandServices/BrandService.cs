using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.BrandServices;

public class BrandService : IBrandService
{
    private readonly IMongoCollection<Brand> _brands;
    private readonly BrandMapper _mapper;

    public BrandService(BrandMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _brands = db.GetCollection<Brand>(settings.BrandCollectionName);
    }

    public async Task<string> CreateBrandAsync(CreateBrandDto brand, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(brand);
        await _brands.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.BrandId;
    }

    public async Task<bool> DeleteBrandAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _brands.DeleteOneAsync(x => x.BrandId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        var items = await _brands.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdBrandDto?> GetBrandByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _brands.Find(x => x.BrandId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateBrandAsync(UpdateBrandDto brand, CancellationToken cancellationToken)
    {
        var filter = Builders<Brand>.Filter.Eq(x => x.BrandId, brand.BrandId);
        var update = Builders<Brand>.Update
            .Set(x => x.BrandName, brand.BrandName)
            .Set(x => x.ImageUrl, brand.ImageUrl);
        var result = await _brands.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
