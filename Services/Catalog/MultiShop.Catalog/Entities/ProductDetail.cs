using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class ProductDetail
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Information { get; set; } = default!;
    public string ProductId { get; set; } = default!;
}