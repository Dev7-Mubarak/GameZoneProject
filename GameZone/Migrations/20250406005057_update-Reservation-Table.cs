using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    public partial class updateReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PaymentMethods_PaymentMethodId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 6, 3, 50, 55, 986, DateTimeKind.Local).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 6, 3, 50, 55, 986, DateTimeKind.Local).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 4, 6, 3, 50, 55, 986, DateTimeKind.Local).AddTicks(5577));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 4, 6, 3, 50, 55, 986, DateTimeKind.Local).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 4, 6, 3, 50, 55, 986, DateTimeKind.Local).AddTicks(5585));


            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PaymentMethods_PaymentMethodId",
                table: "Reservations",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PaymentMethods_PaymentMethodId",
                table: "Reservations");


            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7289));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 3, 26, 5, 2, 27, 937, DateTimeKind.Local).AddTicks(7305));


            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PaymentMethods_PaymentMethodId",
                table: "Reservations",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
