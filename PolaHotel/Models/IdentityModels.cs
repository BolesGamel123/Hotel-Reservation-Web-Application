using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolaHotel.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Reservation>().HasKey(q => q.ReservID);
        //    modelBuilder.Entity<Room>().HasKey(q => q.ID);
        //    modelBuilder.Entity<RoomReservation>().HasKey(q => new { q.RoomID, q.ReservID });
        //    modelBuilder.Entity<RoomReservation>().
        //        HasRequired(t => t.Reservation)
        //        .WithMany(t => t.RoomReservations)
        //        .HasForeignKey(t => t.ReservID);
        //    modelBuilder.Entity<RoomReservation>().
        //        HasRequired(t => t.Room)
        //      .WithMany(t => t.RoomReservations)
        //      .HasForeignKey(t => t.RoomID);




        //}
        public DbSet<Customer> customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room_Category> Room_Categories { get; set; }
        public DbSet<RoomReservation> roomReservations { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}