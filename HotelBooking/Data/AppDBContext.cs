using HotelBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<CategoryRoom> Categories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.category)
                .WithMany()
                .HasForeignKey(r => r.category_id);
        }
        public int getCategoryIdRoom(string categoryName)
        {
            return Categories.FirstOrDefault(c => c.category_name == categoryName).category_id;
        }
    }
}
