using BackendProject.BackgroundJobs;
using BackendProject.Services;
using BookingReminder.RedisCache;
using BookingReminder.Repositories;
using NLog;

namespace BackendProject.DependancyInjection
{
    public static class DependancyInjeationRegester
    {
        public static IServiceCollection AddCustomServicesInjecation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUpcomingBookingRepository,UpcomingBooking>();
            services.AddScoped<BookingReminderService>();
            services.AddSingleton<Logger>(LogManager.GetCurrentClassLogger());
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
