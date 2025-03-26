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
            string UserRoleId = Guid.NewGuid().ToString();

            string adminUserId = Guid.NewGuid().ToString();
            string ownerUserId = Guid.NewGuid().ToString();
            string UserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = ownerRoleId, Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole { Id = UserRoleId, Name = "User", NormalizedName = "USER" }
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
            var User = new AppUser
            {
                Id = UserId,
                FisrtName = "Mubarak",
                LastName = "Bamazhem",
                UserName = "User@example.com",
                NormalizedUserName = "USER@EXAMPLE.COM",
                Email = "User@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true
            };

            var hasher = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");
            ownerUser.PasswordHash = hasher.HashPassword(ownerUser, "Owner@123");
            User.PasswordHash = hasher.HashPassword(User, "User@123");

            builder.Entity<AppUser>().HasData(adminUser, ownerUser, User);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId },
                new IdentityUserRole<string> { RoleId = ownerRoleId, UserId = ownerUserId },
                new IdentityUserRole<string> { RoleId = UserRoleId, UserId = UserId }
            );

            builder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Action" },
               new Category { Id = 2, Name = "Adventure" },
               new Category { Id = 3, Name = "RPG" },
               new Category { Id = 4, Name = "Sports" },
               new Category { Id = 5, Name = "Strategy" },
               new Category { Id = 6, Name = "Shooter" },
               new Category { Id = 7, Name = "Racing" });

            builder.Entity<GameStation>().HasData(
                new GameStation { Id = 1, Name = "Pixel World", Descraption = "Retro and modern games", Location = "Tech Park", Rating = 4.6f, MorningOpenTime = new TimeSpan(11, 0, 0), MorningCloseTime = new TimeSpan(15, 0, 0), EveningOpenTime = new TimeSpan(19, 0, 0), EveningCloseTime = new TimeSpan(1, 0, 0), UserId = "8b178fa1-99b6-4d93-abdb-3c834c1c853a", Image = "pixelworld.jpg", PhoneNumber1 = "4567890123", PhoneNumber2 = "3210987654" }
            );
            builder.Entity<Room>().HasData(
                    new Room
                    {
                        Id = 1,
                        Name = "Room A",
                        Price = 100.00f,
                        NumberOfAllowedPeople = 2,
                        GameStationId = 1,
                        PrimaryImage = "roomA.jpg",
                        Unit = 4
                    },
                    new Room
                    {
                        Id = 2,
                        Name = "Room B",
                        Price = 150.00f,
                        NumberOfAllowedPeople = 3,
                        GameStationId = 1,
                        PrimaryImage = "roomB.jpg",
                        Unit = 3
                    },
                    new Room
                    {
                        Id = 3,
                        Name = "Room C",
                        Price = 120.00f,
                        NumberOfAllowedPeople = 4,
                        GameStationId = 1,
                        PrimaryImage = "roomC.jpg",
                        Unit = 5
                    },
                    new Room
                    {
                        Id = 4,
                        Name = "Room D",
                        Price = 180.00f,
                        NumberOfAllowedPeople = 5,
                        GameStationId = 1,
                        PrimaryImage = "roomD.jpg",
                        Unit = 4
                    }
                );
            builder.Entity<PaymentMethod>().HasData(
                    new PaymentMethod
                    {
                        Id = 1,
                        Name = "Cash"
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        Name = "Transfer"
                    }
                );
            builder.Entity<Reservation>().HasData(
                 new Reservation
                 {
                     Id = 1,
                     UserId = "4a1e51b4-d214-471b-b8d8-2ded79626452",
                     RoomId = 1,
                     GameStationId = 1,
                     StartHour = new DateTime(2025, 3, 26, 10, 0, 0),
                     EndHour = new DateTime(2025, 3, 26, 14, 0, 0),
                     NumberOfHours = 4,
                     Satuts = ReservationStatus.Denied,
                     TotalPrice = 100.0f,
                     Date = DateTime.Now,
                     PaymentMethodId = 2,
                     DepositImage = "deposit_image_1.jpg"
                 },
                 new Reservation
                 {
                     Id = 2,
                     UserId = "4a1e51b4-d214-471b-b8d8-2ded79626452",
                     RoomId = 2,
                     GameStationId = 1,
                     StartHour = new DateTime(2025, 3, 27, 12, 0, 0),
                     EndHour = new DateTime(2025, 3, 27, 18, 0, 0),
                     NumberOfHours = 6,
                     Satuts = ReservationStatus.Confirmed,
                     TotalPrice = 150.0f,
                     Date = DateTime.Now,
                     PaymentMethodId = 2,
                     DepositImage = "deposit_image_2.jpg"
                 },
                 new Reservation
                 {
                     Id = 3,
                     UserId = "4a1e51b4-d214-471b-b8d8-2ded79626452",
                     RoomId = 3,
                     GameStationId = 1,
                     StartHour = new DateTime(2025, 3, 28, 9, 0, 0),
                     EndHour = new DateTime(2025, 3, 28, 17, 0, 0),
                     NumberOfHours = 8,
                     Satuts = ReservationStatus.Completed,
                     TotalPrice = 200.0f,
                     Date = DateTime.Now,
                     PaymentMethodId = 2,
                     DepositImage = "deposit_image_3.jpg"
                 },
                 new Reservation
                 {
                     Id = 4,
                     UserId = "4a1e51b4-d214-471b-b8d8-2ded79626452",
                     RoomId = 4,
                     GameStationId = 1,
                     StartHour = new DateTime(2025, 3, 29, 11, 0, 0),
                     EndHour = new DateTime(2025, 3, 29, 15, 0, 0),
                     NumberOfHours = 4,
                     Satuts = ReservationStatus.Pending,
                     TotalPrice = 120.0f,
                     Date = DateTime.Now,
                     PaymentMethodId = 1,
                 },
                  new Reservation
                  {
                      Id = 5,
                      UserId = "4a1e51b4-d214-471b-b8d8-2ded79626452",
                      RoomId = 1,
                      GameStationId = 1,
                      StartHour = new DateTime(2025, 3, 29, 11, 0, 0),
                      EndHour = new DateTime(2025, 3, 29, 15, 0, 0),
                      NumberOfHours = 4,
                      Satuts = ReservationStatus.Pending,
                      TotalPrice = 120.0f,
                      Date = DateTime.Now,
                      PaymentMethodId = 1,
                  }
            );
            builder.Entity<AccountsInformation>().HasData(
                new AccountsInformation
                {
                    Id = 1,
                    ProviderName = "Omgy",
                    GameStationId = 1,
                    AccountNumber = "1234567890"
                },
                new AccountsInformation
                {
                    Id = 2,
                    ProviderName = "Karamy",
                    GameStationId = 1,
                    AccountNumber = "2345678901"
                }
            );
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
    }

}
