using Newtonsoft.Json;
using StackExchange.Redis;

namespace BookingReminder.RedisCache
{
    public class RedisCache :IRedisCache
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly ILogger<RedisCache> _logger;


        public RedisCache(ILogger<RedisCache> logger)
        { 
            _logger = logger;
            try
            {
                if (ConnectionHelper.connection == null)
                    throw new Exception("Redis connection is not initialized");

                this._database = ConnectionHelper.connection.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception();
            }
           
        }

        public async Task SetDataAsync<T>(string key, T data, TimeSpan time)
        {
            try
            {
                if (_database == null)
                    return ;
                string value = JsonConvert.SerializeObject(data);
                bool result = await _database.StringSetAsync(key, value,time);
                return ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ;
            }
        }
        public async Task<T> GetDataAsync<T>(string key) where T :new()
        {
            try
            {
                if (_database==null)
                    return new T();

                var value = await _database.StringGetAsync(key);
                if (string.IsNullOrEmpty(value))
                    return new T();

                return JsonConvert.DeserializeObject<T>(value);
                
            }
            catch (Exception ex)
            {
                return new T();
            }
           
        }
        public async Task RemoveDataAsync(string key)
        {
            try
            {
                if (_database == null)
                    return;

                bool result = await _database.KeyDeleteAsync(key);
                return ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return;
            }
        }
    }
}
