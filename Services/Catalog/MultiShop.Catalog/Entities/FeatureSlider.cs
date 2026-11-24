using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class FeatureSlider
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string FeatureSliderId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public bool Status { get; set; }
}
