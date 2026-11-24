using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices;

public class OfferDiscountService : IOfferDiscountService
{
    private readonly IMongoCollection<OfferDiscount> _offerDiscounts;
    private readonly OfferDiscountMapper _mapper;

    public OfferDiscountService(OfferDiscountMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _offerDiscounts = db.GetCollection<OfferDiscount>(settings.OfferDiscountCollectionName);
    }

    public async Task<string> CreateOfferDiscountAsync(CreateOfferDiscountDto offerDiscount, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(offerDiscount);
        await _offerDiscounts.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.OfferDiscountId;
    }

    public async Task<bool> DeleteOfferDiscountAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _offerDiscounts.DeleteOneAsync(x => x.OfferDiscountId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultOfferDiscountDto>> GetAllOfferDiscountsAsync(CancellationToken cancellationToken)
    {
        var items = await _offerDiscounts.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdOfferDiscountDto?> GetOfferDiscountByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _offerDiscounts.Find(x => x.OfferDiscountId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateOfferDiscountAsync(UpdateOfferDiscountDto offerDiscount, CancellationToken cancellationToken)
    {
        var filter = Builders<OfferDiscount>.Filter.Eq(x => x.OfferDiscountId, offerDiscount.OfferDiscountId);
        var update = Builders<OfferDiscount>.Update
            .Set(x => x.Title, offerDiscount.Title)
            .Set(x => x.SubTitle, offerDiscount.SubTitle)
            .Set(x => x.ImageUrl, offerDiscount.ImageUrl)
            .Set(x => x.ButtonTitle, offerDiscount.ButtonTitle);
        var result = await _offerDiscounts.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
