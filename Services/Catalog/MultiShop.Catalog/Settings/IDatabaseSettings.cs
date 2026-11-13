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
}
