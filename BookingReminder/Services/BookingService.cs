using BackendProject.AppSettings;
using BackendProject.Models;
using BookingReminder.RedisCache;
using BookingReminder.Repositories;

namespace BackendProject.Services
{
    public class BookingReminderService
    {
        private readonly List<RestaurantSetting> _restaurantSettings;
        private readonly IUpcomingBookingRepository _upcomingBooking;


        public BookingReminderService(List<RestaurantSetting> restaurantSettings, IUpcomingBookingRepository upcomingBooking)
        {
            _restaurantSettings = restaurantSettings;
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





