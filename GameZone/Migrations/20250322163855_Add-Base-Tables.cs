using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    public partial class AddBaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e03b89f-8fff-4d22-9a74-72579fbc1c04");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f588e602-dda1-404b-80bf-88fc39d1da7c", "8693d7ce-431a-448c-a3fd-e398b1decdb6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "49104c7b-000c-47d3-80a3-6bec7bfe7e78", "d560dbc1-92c6-4477-8d29-d448e4dd14f9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49104c7b-000c-47d3-80a3-6bec7bfe7e78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f588e602-dda1-404b-80bf-88fc39d1da7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8693d7ce-431a-448c-a3fd-e398b1decdb6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d560dbc1-92c6-4477-8d29-d448e4dd14f9");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descraption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: true),
                    MorningOpenTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    MorningCloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EveningOpenTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EveningCloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descraption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Release = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    GameStationId = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsInformation_GameStations_GameStationId",
                        column: x => x.GameStationId,
                        principalTable: "GameStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameStationId = table.Column<int>(type: "int", nullable: false),
                    Rating1 = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_GameStations_GameStationId",
                        column: x => x.GameStationId,
                        principalTable: "GameStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<float>(type: "real", nullable: false),
                    NumberOfAllowedPeople = table.Column<short>(type: "smallint", nullable: false),
                    GameStationId = table.Column<int>(type: "int", nullable: false),
                    PrimaryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomTypeId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_GameStations_GameStationId",
                        column: x => x.GameStationId,
                        principalTable: "GameStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceGame",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGame", x => new { x.DevicesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_DeviceGame_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStationGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GameStationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStationGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStationGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStationGames_GameStations_GameStationId",
                        column: x => x.GameStationId,
                        principalTable: "GameStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRoom",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRoom", x => new { x.DevicesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_DeviceRoom_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfHours = table.Column<short>(type: "smallint", nullable: false),
                    Satuts = table.Column<byte>(type: "tinyint", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    DepositImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RoomsPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomsPictures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bed3555-7f91-4c97-a7ef-8fe8c72279c8", "72124b4d-8d45-4506-994c-74cc1868865f", "User", "USER" },
                    { "9adf4341-ad5b-4c5c-a42a-469c5453d170", "307dfbf3-e579-4537-8507-62c3a5cf9b00", "Owner", "OWNER" },
                    { "9b094a88-ac1b-4469-a3d2-66945b9f2275", "e8ae4289-3125-4ce0-b317-77ebcbee7ee1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FisrtName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5226c55e-aefc-44f1-acc6-ae3b586b6a5e", 0, "6076871b-f3f7-4899-b7b0-d53379e13a47", "owner@example.com", true, "Mubarak", "Bamazhem", false, null, "OWNER@EXAMPLE.COM", "OWNER@EXAMPLE.COM", "AQAAAAEAACcQAAAAELjBMm/0vaTVqWfy6sspnMHw8M9jzpoyYslKZp4frdcs5QoGUNGRvxv5REiDjehOTA==", null, false, null, "f7062579-f4bd-4b34-8e3b-806111e6021e", false, "owner@example.com" },
                    { "f0ef7f5b-e986-41b2-a10b-fd1644fbdb84", 0, "3eea1ad5-82c4-4c82-b156-9d4a0af9c7bd", "admin@example.com", true, "Mubarak", "Bamazhem", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEMndfdV3ZjEI3uTLdmtqIHTj9lIaltGyHfNBrakYcC7HufdSoW/LLZ5G1wtfdfLxLQ==", null, false, null, "37f4803b-1da1-4654-95db-d5b3e4e75f04", false, "admin@example.com" }
                });

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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9adf4341-ad5b-4c5c-a42a-469c5453d170", "5226c55e-aefc-44f1-acc6-ae3b586b6a5e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9b094a88-ac1b-4469-a3d2-66945b9f2275", "f0ef7f5b-e986-41b2-a10b-fd1644fbdb84" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInformation_GameStationId",
                table: "AccountsInformation",
                column: "GameStationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGame_GamesId",
                table: "DeviceGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRoom_RoomsId",
                table: "DeviceRoom",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CategoryId",
                table: "Games",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStationGames_GameId",
                table: "GameStationGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStationGames_GameStationId",
                table: "GameStationGames",
                column: "GameStationId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStations_UserId",
                table: "GameStations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_GameStationId",
                table: "Ratings",
                column: "GameStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PaymentMethodId",
                table: "Reservations",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GameStationId",
                table: "Rooms",
                column: "GameStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsPictures_RoomId",
                table: "RoomsPictures",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsInformation");

            migrationBuilder.DropTable(
                name: "DeviceGame");

            migrationBuilder.DropTable(
                name: "DeviceRoom");

            migrationBuilder.DropTable(
                name: "GameStationGames");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RoomsPictures");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GameStations");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bed3555-7f91-4c97-a7ef-8fe8c72279c8");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9adf4341-ad5b-4c5c-a42a-469c5453d170", "5226c55e-aefc-44f1-acc6-ae3b586b6a5e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b094a88-ac1b-4469-a3d2-66945b9f2275", "f0ef7f5b-e986-41b2-a10b-fd1644fbdb84" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9adf4341-ad5b-4c5c-a42a-469c5453d170");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b094a88-ac1b-4469-a3d2-66945b9f2275");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5226c55e-aefc-44f1-acc6-ae3b586b6a5e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f0ef7f5b-e986-41b2-a10b-fd1644fbdb84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49104c7b-000c-47d3-80a3-6bec7bfe7e78", "aee07a8b-b5b5-4f0b-842d-04d0e2e3ff29", "Admin", "ADMIN" },
                    { "4e03b89f-8fff-4d22-9a74-72579fbc1c04", "6344912e-a0f1-4d72-bbbb-c7f86a0957b1", "User", "USER" },
                    { "f588e602-dda1-404b-80bf-88fc39d1da7c", "ab965dbd-5f15-4cee-9a3b-5276943b8cc4", "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FisrtName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8693d7ce-431a-448c-a3fd-e398b1decdb6", 0, "74416f7a-847f-4348-931c-b493f4241838", "owner@example.com", true, "Mubarak", "Bamazhem", false, null, "OWNER@EXAMPLE.COM", "OWNER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEDLCWTYGFqcZecn3itFqtvzq+2EaJBN8RyzlMJFZ1ZpjnFV0EnOy98WgSJGKYvRaZA==", null, false, null, "ac3ddd4d-0ca1-4a6d-bd16-206c362d8e4f", false, "owner@example.com" },
                    { "d560dbc1-92c6-4477-8d29-d448e4dd14f9", 0, "3764a2a5-a979-4364-9d20-7fd2727d8031", "admin@example.com", true, "Mubarak", "Bamazhem", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEN81ZBR8Dheh5S/CZdFIfBg776gnaUVQFo4AylV3HaW8MnOULaZ1JzsQbGkChShP4A==", null, false, null, "6655b276-0cd4-41f2-8581-305557483ef2", false, "admin@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f588e602-dda1-404b-80bf-88fc39d1da7c", "8693d7ce-431a-448c-a3fd-e398b1decdb6" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "49104c7b-000c-47d3-80a3-6bec7bfe7e78", "d560dbc1-92c6-4477-8d29-d448e4dd14f9" });
        }
    }
}
