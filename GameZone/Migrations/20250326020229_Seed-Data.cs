using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "RPG" },
                    { 4, "Sports" },
                    { 5, "Strategy" },
                    { 6, "Shooter" },
                    { 7, "Racing" }
                });

            migrationBuilder.InsertData(
                table: "GameStations",
                columns: new[] { "Id", "Descraption", "EveningCloseTime", "EveningOpenTime", "Image", "Location", "MorningCloseTime", "MorningOpenTime", "Name", "PhoneNumber1", "PhoneNumber2", "Rating", "UserId" },
                values: new object[] { 1, "Retro and modern games", new TimeSpan(0, 1, 0, 0, 0), new TimeSpan(0, 19, 0, 0, 0), "pixelworld.jpg", "Tech Park", new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 11, 0, 0, 0), "Pixel World", "4567890123", "3210987654", 4.6f, "8b178fa1-99b6-4d93-abdb-3c834c1c853a" });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Transfer" }
                });

            migrationBuilder.InsertData(
                table: "AccountsInformation",
                columns: new[] { "Id", "AccountNumber", "GameStationId", "ProviderName" },
                values: new object[,]
                {
                    { 1, "1234567890", 1, "Omgy" },
                    { 2, "2345678901", 1, "Karamy" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "GameStationId", "Name", "NumberOfAllowedPeople", "Price", "PrimaryImage", "Unit" },
                values: new object[,]
                {
                    { 1, 1, "Room A", (short)2, 100f, "roomA.jpg", (byte)4 },
                    { 2, 1, "Room B", (short)3, 150f, "roomB.jpg", (byte)3 },
                    { 3, 1, "Room C", (short)4, 120f, "roomC.jpg", (byte)5 },
                    { 4, 1, "Room D", (short)5, 180f, "roomD.jpg", (byte)4 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Date", "DepositImage", "EndHour", "GameStationId", "NumberOfHours", "PaymentMethodId", "RoomId", "Satuts", "StartHour", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7265), "deposit_image_1.jpg", new DateTime(2025, 3, 26, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)4, 2, 1, 2, new DateTime(2025, 3, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), 100f, "4a1e51b4-d214-471b-b8d8-2ded79626452" },
                    { 2, new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7289), "deposit_image_2.jpg", new DateTime(2025, 3, 27, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)6, 2, 2, 1, new DateTime(2025, 3, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 150f, "4a1e51b4-d214-471b-b8d8-2ded79626452" },
                    { 3, new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7294), "deposit_image_3.jpg", new DateTime(2025, 3, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)8, 2, 3, 3, new DateTime(2025, 3, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), 200f, "4a1e51b4-d214-471b-b8d8-2ded79626452" },
                    { 4, new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7301), null, new DateTime(2025, 3, 29, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)4, 1, 4, 4, new DateTime(2025, 3, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), 120f, "4a1e51b4-d214-471b-b8d8-2ded79626452" },
                    { 5, new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7305), null, new DateTime(2025, 3, 29, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)4, 1, 1, 4, new DateTime(2025, 3, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), 120f, "4a1e51b4-d214-471b-b8d8-2ded79626452" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsInformation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountsInformation",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);



            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameStations",
                keyColumn: "Id",
                keyValue: 1);

        }
    }
}
