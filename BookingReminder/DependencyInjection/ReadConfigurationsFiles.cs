using BackendProject.AppSettings;
using BackendProject.Models;
using BookingReminder.AppSettings;
using Microsoft.EntityFrameworkCore;

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

            services.AddDbContext<ApplicationDbContext>(Options =>
     Options.UseSqlServer(configuration.GetConnectionString("DataConnection")));
            return services;
        }
    }
}
