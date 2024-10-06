using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class MakePhoneNullableInCustomersV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 5, 21, 40, 12, 984, DateTimeKind.Local).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 6, 21, 40, 12, 984, DateTimeKind.Local).AddTicks(9166));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 7, 21, 40, 12, 984, DateTimeKind.Local).AddTicks(9171));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 5, 21, 30, 22, 945, DateTimeKind.Local).AddTicks(1093));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 6, 21, 30, 22, 945, DateTimeKind.Local).AddTicks(1144));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 7, 21, 30, 22, 945, DateTimeKind.Local).AddTicks(1148));
        }
    }
}
