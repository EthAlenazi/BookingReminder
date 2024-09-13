using BackendProject.Models;
using BackendProject.Settings;
using BookingReminder.RedisCache;
using BookingReminder.Repositories;

namespace BackendProject.Services
{
    public class BookingReminderService
    {
        private readonly List<RestaurantSetting> _restaurantSettings;
        private readonly ApplicationDbContext _context;
        private readonly IRedisCache _cache;
        private const string cacheKey = "UpcomingBookingsEmails";
        private readonly UpcomingBooking _upcomingBooking;


        public BookingReminderService(List<RestaurantSetting> restaurantSettings,ApplicationDbContext context,
             IRedisCache cache, UpcomingBooking upcomingBooking)
        {
            _restaurantSettings = restaurantSettings;
            _context = context;
            _cache = cache;
            _upcomingBooking = upcomingBooking;
        }

        public async Task<List<string>> GetEmailsForUpcomingBookingsAsync(DateTime currentTime)
        {
            var emails = new List<string>();

            var bookings = await _upcomingBooking.GetBookingsAsync();
            foreach (var booking in bookings)
            {
                var restaurantSetting = _restaurantSettings
                    .FirstOrDefault(rs => rs.RestaurantID == booking.RestaurantID);

                if (restaurantSetting != null)
                {
                    var reminderTime = booking.BookingDate
                        .AddMinutes(-restaurantSetting.BookingReminderMinutes);

                    if (reminderTime <= currentTime)
                    {
                        emails.Add(booking.User.Email);
                    }
                }
            }

            return emails;
        }

    }
}





