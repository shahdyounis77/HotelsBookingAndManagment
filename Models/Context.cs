using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Hotel.Models
{
    public class Context:IdentityDbContext<User>
    {
            public Context(DbContextOptions<Context> options) : base(options)
            {
            }
    
            public DbSet<Hotel> Hotels { get; set; }
            public DbSet<Room> Rooms { get; set; }
            public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Booking>()
        .HasOne(b => b.Hotel)
        .WithMany(h => h.Bookings)
        .HasForeignKey(b => b.HotelId)
        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
