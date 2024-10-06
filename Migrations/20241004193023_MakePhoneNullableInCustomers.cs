using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class MakePhoneNullableInCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 5, 21, 17, 38, 12, DateTimeKind.Local).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 6, 21, 17, 38, 12, DateTimeKind.Local).AddTicks(9224));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 7, 21, 17, 38, 12, DateTimeKind.Local).AddTicks(9228));
        }
    }
}
