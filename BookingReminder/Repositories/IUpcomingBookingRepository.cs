using BackendProject.Models.Data;

namespace BookingReminder.Repositories
{
    public interface IUpcomingBookingRepository
    {
        Task<List<Booking>> GetBookingsAsync();
    }
}
