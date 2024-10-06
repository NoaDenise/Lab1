using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class MakeTableIdNullableInReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int?>(
                name: "TableId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Ditt existerande datumuppdateringskod
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


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            // Ditt existerande datumuppdateringskod
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
        }

    }
}
