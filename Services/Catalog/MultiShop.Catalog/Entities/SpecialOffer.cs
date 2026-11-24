using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class SpecialOffer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SpecialOfferId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
