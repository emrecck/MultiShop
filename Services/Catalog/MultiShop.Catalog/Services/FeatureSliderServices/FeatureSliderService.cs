using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices;

public class FeatureSliderService : IFeatureSliderService
{
    private readonly IMongoCollection<FeatureSlider> _sliders;
    private readonly FeatureSliderMapper _mapper;

    public FeatureSliderService(FeatureSliderMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _sliders = db.GetCollection<FeatureSlider>(settings.FeatureSliderCollectionName);
    }

    public async Task<string> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSlider, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(featureSlider);
        await _sliders.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.FeatureSliderId;
    }

    public async Task<bool> DeleteFeatureSliderAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _sliders.DeleteOneAsync(x => x.FeatureSliderId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultFeatureSliderDto>> GetAllFeatureSlidersAsync(CancellationToken cancellationToken)
    {
        var items = await _sliders.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdFeatureSliderDto?> GetFeatureSliderByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _sliders.Find(x => x.FeatureSliderId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSlider, CancellationToken cancellationToken)
    {
        var filter = Builders<FeatureSlider>.Filter.Eq(x => x.FeatureSliderId, featureSlider.FeatureSliderId);
        var update = Builders<FeatureSlider>.Update
            .Set(x => x.Title, featureSlider.Title)
            .Set(x => x.Description, featureSlider.Description)
            .Set(x => x.ImageUrl, featureSlider.ImageUrl)
            .Set(x => x.Status, featureSlider.Status);
        var result = await _sliders.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }

    public async Task<bool> ChangeStatusAsync(string id, bool status, CancellationToken cancellationToken)
    {
        var filter = Builders<FeatureSlider>.Filter.Eq(x => x.FeatureSliderId, id);
        var update = Builders<FeatureSlider>.Update.Set(x => x.Status, status);
        var result = await _sliders.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
