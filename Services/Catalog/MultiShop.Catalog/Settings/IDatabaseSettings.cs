using System;

namespace MultiShop.Catalog.Settings;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string ProductCollectionName { get; set; }
    string CategoryCollectionName { get; set; }
    string ProductDetailCollectionName { get; set; }
    string ProductImageCollectionName { get; set; }
    string AboutCollectionName { get; set; }
    string BrandCollectionName { get; set; }
    string ContactCollectionName { get; set; }
    string FeatureCollectionName { get; set; }
    string FeatureSliderCollectionName { get; set; }
    string OfferDiscountCollectionName { get; set; }
    string SpecialOfferCollectionName { get; set; }
}
