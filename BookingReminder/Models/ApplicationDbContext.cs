using BackendProject.Models.Data;
using BackendProject.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
