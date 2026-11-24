using System;

namespace MultiShop.Catalog.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string ProductCollectionName { get; set; } = default!;
    public string CategoryCollectionName { get; set; } = default!;
    public string ProductDetailCollectionName { get; set; } = default!;
    public string ProductImageCollectionName { get; set; } = default!;
    public string AboutCollectionName { get; set; } = "Abouts";
    public string BrandCollectionName { get; set; } = "Brands";
    public string ContactCollectionName { get; set; } = "Contacts";
    public string FeatureCollectionName { get; set; } = "Features";
    public string FeatureSliderCollectionName { get; set; } = "FeatureSliders";
    public string OfferDiscountCollectionName { get; set; } = "OfferDiscounts";
    public string SpecialOfferCollectionName { get; set; } = "SpecialOffers";
}
