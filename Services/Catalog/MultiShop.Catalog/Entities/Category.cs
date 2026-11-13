using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class Category
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}
