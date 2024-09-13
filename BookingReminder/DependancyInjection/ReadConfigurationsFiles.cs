using BackendProject.Models;
using BackendProject.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

            return services;
        }
    }
}
