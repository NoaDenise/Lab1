using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 4, 19, 0, 15, 394, DateTimeKind.Local).AddTicks(4952));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 5, 19, 0, 15, 394, DateTimeKind.Local).AddTicks(5039));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 6, 19, 0, 15, 394, DateTimeKind.Local).AddTicks(5046));

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 3, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 4, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3,
                column: "ReservationDate",
                value: new DateTime(2024, 10, 5, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8227));
        }
    }
}
