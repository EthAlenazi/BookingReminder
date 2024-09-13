using BackendProject.Helpers;
using BackendProject.Services;
using BookingReminder.RedisCache;
using BookingReminder.Repositories;

namespace BackendProject.DependancyInjection
{
    public static class DependancyInjeationRegester
    {
        public static IServiceCollection AddCustomServicesInjecation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UpcomingBooking>();
            services.AddScoped<BookingReminderService>();

            services.AddScoped<ReminderDelegate>(provider =>
            {
                var reminderService = provider.GetRequiredService<BookingReminderService>();
                return new ReminderDelegate(async currentTime =>
            await reminderService.GetEmailsForUpcomingBookingsAsync(currentTime));
            });

            services.AddScoped<IRedisCache, RedisCache>();
            services.AddHostedService<BackgroundReminderService>();

            return services;
        }
    }
}
