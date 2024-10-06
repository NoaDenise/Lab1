using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddMockDataForTablesAndReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Seats", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CustomerId", "NumberOfGuests", "ReservationDate", "TableId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2024, 10, 3, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8156), 1 },
                    { 2, 2, 2, new DateTime(2024, 10, 4, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8224), 2 },
                    { 3, 3, 3, new DateTime(2024, 10, 5, 14, 35, 53, 161, DateTimeKind.Local).AddTicks(8227), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3);
        }
    }
}
