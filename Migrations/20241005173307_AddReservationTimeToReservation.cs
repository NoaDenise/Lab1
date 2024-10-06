using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationTimeToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReservationTime",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "ReservationDate", "ReservationTime" },
                values: new object[] { new DateTime(2024, 10, 6, 19, 33, 7, 296, DateTimeKind.Local).AddTicks(8924), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                columns: new[] { "ReservationDate", "ReservationTime" },
                values: new object[] { new DateTime(2024, 10, 7, 19, 33, 7, 296, DateTimeKind.Local).AddTicks(9004), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                columns: new[] { "ReservationDate", "ReservationTime" },
                values: new object[] { new DateTime(2024, 10, 8, 19, 33, 7, 296, DateTimeKind.Local).AddTicks(9019), new TimeSpan(0, 0, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "Reservations");

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
    }
}
