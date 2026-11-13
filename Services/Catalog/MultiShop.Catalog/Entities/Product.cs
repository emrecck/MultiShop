using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = default!;
    public string CategoryId { get; set; } = default!;

    [BsonIgnore]
    public Category Category { get; set; } = default!;
}