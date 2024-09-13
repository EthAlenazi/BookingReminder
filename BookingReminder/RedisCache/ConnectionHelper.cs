using BookingReminder.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BookingReminder.RedisCache
{
    public class ConnectionHelper
    {
        private static Lazy<ConnectionMultiplexer> _redisConnection;

        private static IRedisConfig _redisConfig;
        public static void Init(IRedisConfig redisConfig)
        {
            _redisConfig = redisConfig;
            try
            {
                ConfigurationOptions configuration = new ConfigurationOptions
                {
                    EndPoints = { { _redisConfig.EndPoint, _redisConfig.Port } },
                    AbortOnConnectFail = _redisConfig.AbortOnConnectFail,
                    AsyncTimeout = _redisConfig.AsyncTimeout


                };
                ConnectionHelper._redisConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(configuration);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static ConnectionMultiplexer connection
        {
            get
            {
                return _redisConnection.Value;
            }
        }
    }
}