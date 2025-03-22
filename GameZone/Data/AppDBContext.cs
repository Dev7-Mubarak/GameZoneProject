using GameZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string adminRoleId = Guid.NewGuid().ToString();
            string ownerRoleId = Guid.NewGuid().ToString();

            string adminUserId = Guid.NewGuid().ToString();
            string ownerUserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = ownerRoleId, Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
            );

            var adminUser = new AppUser
            {
                Id = adminUserId,
                FisrtName = "Mubarak",
                LastName = "Bamazhem",
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true
            };
            var ownerUser = new AppUser
            {
                Id = ownerUserId,
                FisrtName = "Mubarak",
                LastName = "Bamazhem",
                UserName = "owner@example.com",
                NormalizedUserName = "OWNER@EXAMPLE.COM",
                Email = "owner@example.com",
                NormalizedEmail = "OWNER@EXAMPLE.COM",
                EmailConfirmed = true
            };

            var hasher = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");
            ownerUser.PasswordHash = hasher.HashPassword(ownerUser, "Owner@123");

            builder.Entity<AppUser>().HasData(adminUser, ownerUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId },
                new IdentityUserRole<string> { RoleId = ownerRoleId, UserId = ownerUserId }
            );
            // Game Categories
            builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action" },
            new Category { Id = 2, Name = "Adventure" },
            new Category { Id = 3, Name = "RPG" },
            new Category { Id = 4, Name = "Sports" },
            new Category { Id = 5, Name = "Strategy" },
            new Category { Id = 6, Name = "Shooter" },
            new Category { Id = 7, Name = "Racing" });
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameStation> GameStations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameStationGame> GameStationGames { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomsPicture> RoomsPictures { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
    }

}
