using BackendProject.Helpers;
using BackendProject.Services;

namespace BackendProject.DependancyInjection
{
    public static class DependancyInjeationRegester
    {
        public static IServiceCollection AddCustomServicesInjecation(this IServiceCollection services, IConfiguration configuration)
        {

            
            services.AddScoped<BookingReminderService>();


            services.AddScoped<ReminderDelegate>(provider =>
            {
                var reminderService = provider.GetRequiredService<BookingReminderService>();
                return new ReminderDelegate(reminderService.GetEmailsForUpcomingBookings);
            });

            services.AddHostedService<BackgroundReminderService>();

            return services;
        }
    }
}
