using BackendProject.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }

}
