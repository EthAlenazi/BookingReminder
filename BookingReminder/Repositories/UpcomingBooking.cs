using BackendProject.Models;
using BackendProject.Models.Data;
using BookingReminder.RedisCache;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookingReminder.Repositories
{
    public class UpcomingBooking
    {
        private readonly ApplicationDbContext _context;
        private readonly IRedisCache _cache;
        private const string CacheKey = "UpcomingBookings";

        public UpcomingBooking(ApplicationDbContext context, IRedisCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Booking>> GetBookingsAsync() 
        {
            //we must implement the same logic if we have any changes on Booking table,
            //where we will use remove method

            List<Booking> bookings = await _cache.GetDataAsync<List<Booking>>(CacheKey);
            if (bookings.Count<=0)
            {
                bookings = await _context.Bookings
                    .Include(b => b.Restaurant)
                    .Include(b => b.User)
                    .ToListAsync();

                var serializedData = JsonConvert.SerializeObject(bookings);
                await _cache.SetDataAsync(CacheKey, serializedData, TimeSpan.FromHours(2));
            }

            return bookings;
        }
    }
}
