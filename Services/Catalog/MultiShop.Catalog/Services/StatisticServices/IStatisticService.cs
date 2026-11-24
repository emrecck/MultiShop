namespace MultiShop.Catalog.Services.StatisticServices;

public interface IStatisticService
{
    Task<long> GetCategoryCountAsync(CancellationToken cancellationToken);
    Task<long> GetProductCountAsync(CancellationToken cancellationToken);
    Task<long> GetBrandCountAsync(CancellationToken cancellationToken);
    Task<decimal> GetProductAvgPriceAsync(CancellationToken cancellationToken);
    Task<string> GetMaxPriceProductNameAsync(CancellationToken cancellationToken);
    Task<string> GetMinPriceProductNameAsync(CancellationToken cancellationToken);
}
