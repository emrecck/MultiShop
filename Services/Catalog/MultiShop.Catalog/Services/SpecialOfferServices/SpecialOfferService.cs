using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices;

public class SpecialOfferService : ISpecialOfferService
{
    private readonly IMongoCollection<SpecialOffer> _specialOffers;
    private readonly SpecialOfferMapper _mapper;

    public SpecialOfferService(SpecialOfferMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _specialOffers = db.GetCollection<SpecialOffer>(settings.SpecialOfferCollectionName);
    }

    public async Task<string> CreateSpecialOfferAsync(CreateSpecialOfferDto specialOffer, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(specialOffer);
        await _specialOffers.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.SpecialOfferId;
    }

    public async Task<bool> DeleteSpecialOfferAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _specialOffers.DeleteOneAsync(x => x.SpecialOfferId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultSpecialOfferDto>> GetAllSpecialOffersAsync(CancellationToken cancellationToken)
    {
        var items = await _specialOffers.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdSpecialOfferDto?> GetSpecialOfferByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _specialOffers.Find(x => x.SpecialOfferId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateSpecialOfferAsync(UpdateSpecialOfferDto specialOffer, CancellationToken cancellationToken)
    {
        var filter = Builders<SpecialOffer>.Filter.Eq(x => x.SpecialOfferId, specialOffer.SpecialOfferId);
        var update = Builders<SpecialOffer>.Update
            .Set(x => x.Title, specialOffer.Title)
            .Set(x => x.SubTitle, specialOffer.SubTitle)
            .Set(x => x.ImageUrl, specialOffer.ImageUrl);
        var result = await _specialOffers.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
