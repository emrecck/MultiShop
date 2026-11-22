using StackExchange.Redis;
using System.Text.Json;

namespace MultiShop.Cargo.Basket.Models
{
    public class RedisService(IConnectionMultiplexer connectionMultiplexer) : IRedisService
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        
        public async Task Clear(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task ClearAll()
        {
            await _database.ExecuteAsync("FLUSHDB");
        }

        public async Task<T?> GetValueAsync<T>(string key)
        {
            var result = await _database.StringGetAsync(key);
            if (result.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(result.ToString());
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _database.StringSetAsync(key, value);
        }
    }
}
