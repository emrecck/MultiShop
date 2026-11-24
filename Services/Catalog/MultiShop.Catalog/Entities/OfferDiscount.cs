using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class OfferDiscount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OfferDiscountId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string ButtonTitle { get; set; } = default!;
}
