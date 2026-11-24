using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices;

public class FeatureService : IFeatureService
{
    private readonly IMongoCollection<Feature> _features;
    private readonly FeatureMapper _mapper;

    public FeatureService(FeatureMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _features = db.GetCollection<Feature>(settings.FeatureCollectionName);
    }

    public async Task<string> CreateFeatureAsync(CreateFeatureDto feature, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(feature);
        await _features.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.FeatureId;
    }

    public async Task<bool> DeleteFeatureAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _features.DeleteOneAsync(x => x.FeatureId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultFeatureDto>> GetAllFeaturesAsync(CancellationToken cancellationToken)
    {
        var items = await _features.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdFeatureDto?> GetFeatureByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _features.Find(x => x.FeatureId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateFeatureAsync(UpdateFeatureDto feature, CancellationToken cancellationToken)
    {
        var filter = Builders<Feature>.Filter.Eq(x => x.FeatureId, feature.FeatureId);
        var update = Builders<Feature>.Update
            .Set(x => x.Title, feature.Title)
            .Set(x => x.Icon, feature.Icon);
        var result = await _features.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
