using BackendProject.Models;
using BackendProject.Models.Data;
using BookingReminder.RedisCache;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookingReminder.Repositories
{
    public class UpcomingBooking: IUpcomingBookingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRedisCache _cache;
        private const string CacheKey = "UpcomingBookings";
        private readonly ILogger<UpcomingBooking> _logger;

        public UpcomingBooking(ApplicationDbContext context, IRedisCache cache, ILogger<UpcomingBooking> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<Booking>> GetBookingsAsync() 
        {
            //we must implement the same logic if we have any changes on Booking table,
            //where we will use remove method
            try
            {
                List<Booking> bookings = await _cache.GetDataAsync<List<Booking>>(CacheKey);
                if (bookings.Count<1)
                {
                    bookings = await _context.Bookings
                        .Include(b => b.Restaurant)
                        .Include(b => b.User)
                        .ToListAsync();
                    //According to our theory, We can obtain all reservations for today alone,
                    //but since each restaurant has a unique configuration,
                    //we can also get updated data when any insert event.


                    await _cache.SetDataAsync(CacheKey, bookings, TimeSpan.FromHours(2));
                }
                return bookings;
            }
            catch (Exception ex) {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
