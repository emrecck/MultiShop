namespace MultiShop.Cargo.Basket.Models
{
    public interface IRedisService
    {
        Task<T?> GetValueAsync<T>(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task Clear(string key);
        Task ClearAll();
    }
}
