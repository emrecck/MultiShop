using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class Feature
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string FeatureId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Icon { get; set; } = default!;
}
