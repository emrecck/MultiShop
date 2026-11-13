using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class ProductImage
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string Image1 { get; set; } = default!;
    public string Image2 { get; set; } = default!;
    public string Image3 { get; set; } = default!;
    public string ProductId { get; set; } = default!;

    [BsonIgnore]
    public Product Product { get; set; } = default!;
}
