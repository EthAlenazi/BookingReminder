using BackendProject.Models;
using BackendProject.Settings;
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

            services.AddDbContext<ApplicationDbContext>(Options =>
                Options.UseSqlServer(configuration.GetConnectionString("DataConnection")));

            var redisConnectionString = configuration.GetConnectionString("Redis");

            var options = ConfigurationOptions.Parse(redisConnectionString);
            options.AbortOnConnectFail = false;
            options.ConnectTimeout = 10000; // 
            options.SyncTimeout = 10000; // 
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
           
            return services;
        }
    }
}
