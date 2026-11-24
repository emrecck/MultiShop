using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BrandId { get; set; } = default!;
    public string BrandName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
