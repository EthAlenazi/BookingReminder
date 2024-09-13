using StackExchange.Redis;

namespace BookingReminder.RedisCache
{
    public class RedisCache :IRedisCache
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCache(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<bool> SetDataAsync(string key, string value, TimeSpan time)
        {
            try
            {
                var db = _redis.GetDatabase();
                bool result = await db.StringSetAsync(key, value,time);
                return result;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting data: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetDataAsync(string key)
        {
            try
            {
                var db = _redis.GetDatabase();
                var value = await db.StringGetAsync(key);
                return value.HasValue ? value.ToString() : "Key not found";
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return "Error connecting to Redis";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting data: {ex.Message}");
                return "Error retrieving data";
            }
        }

        public async Task<bool> RemoveDataAsync(string key)
        {
            try
            {
                var db = _redis.GetDatabase();
                bool result = await db.KeyDeleteAsync(key);
                return result;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing data: {ex.Message}");
                return false;
            }
        }
    }
}
