using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices;

public class AboutService : IAboutService
{
    private readonly IMongoCollection<About> _abouts;
    private readonly AboutMapper _mapper;

    public AboutService(AboutMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _abouts = db.GetCollection<About>(settings.AboutCollectionName);
    }

    public async Task<string> CreateAboutAsync(CreateAboutDto about, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(about);
        await _abouts.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.AboutId;
    }

    public async Task<bool> DeleteAboutAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _abouts.DeleteOneAsync(x => x.AboutId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultAboutDto>> GetAllAboutsAsync(CancellationToken cancellationToken)
    {
        var items = await _abouts.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdAboutDto?> GetAboutByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _abouts.Find(x => x.AboutId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateAboutAsync(UpdateAboutDto about, CancellationToken cancellationToken)
    {
        var filter = Builders<About>.Filter.Eq(x => x.AboutId, about.AboutId);
        var update = Builders<About>.Update
            .Set(x => x.Description, about.Description)
            .Set(x => x.Address, about.Address)
            .Set(x => x.Email, about.Email)
            .Set(x => x.Phone, about.Phone);
        var result = await _abouts.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
