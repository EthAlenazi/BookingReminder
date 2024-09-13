using BackendProject.Models;
using BackendProject.Settings;
using BookingReminder.Settings;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace BackendProject.DependancyInjection
{
    public static class ConfigurationsFiles
    {
        public static IServiceCollection ReadConfigurationsFiles(this IServiceCollection services, IConfiguration configuration)
        {
            var restaurantSettings = new List<RestaurantSetting>();
            configuration.GetSection("RestaurantSettings").Bind(restaurantSettings);

            services.AddSingleton(restaurantSettings);

            var RedisSettings = new RedisConfig();
            configuration.GetSection("RedisConfig").Bind(RedisSettings);
            services.AddSingleton<IRedisConfig>(RedisSettings);

            //var redisConnectionString = configuration.GetSection("Redis");

            //var options = ConfigurationOptions.Parse(redisConnectionString);
            //options.AbortOnConnectFail = false;
            //options.ConnectTimeout = 10000; // 
            //options.SyncTimeout = 10000; // 
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
            services.AddDbContext<ApplicationDbContext>(Options =>
     Options.UseSqlServer(configuration.GetConnectionString("DataConnection")));
            return services;
        }
    }
}
