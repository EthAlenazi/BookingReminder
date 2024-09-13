using BackendProject.Models;
using BackendProject.Settings;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendProject.Services
{
    public class BookingReminderService
    {
        private readonly List<RestaurantSetting> _restaurantSettings;
        private readonly ApplicationDbContext _context;

        public BookingReminderService(List<RestaurantSetting> restaurantSettings,ApplicationDbContext context)
        {
            _restaurantSettings = restaurantSettings;
            _context = context;
        }

        public List<string> GetEmailsForUpcomingBookings(DateTime currentTime)
        {
            var emails = new List<string>();

            
            var bookings = _context.Bookings.Include(b => b.Restaurant)
                                            .Include(b => b.User);

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





