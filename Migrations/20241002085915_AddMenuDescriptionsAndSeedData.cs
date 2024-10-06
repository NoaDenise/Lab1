using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuDescriptionsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemId", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "En klassisk pizza med tomat, basilika och mozzarella.", true, "Pizza Margherita", 99m },
                    { 2, "Pasta med en köttfärssås baserad på nötkött och tomater.", true, "Spaghetti Bolognese", 129m },
                    { 3, "Fräsch sallad med kyckling, krutonger och parmesan.", true, "Caesarsallad", 89m },
                    { 4, "Saftig hamburgare serverad med pommes frites.", false, "Hamburgare", 139m },
                    { 5, "Kycklingfilé grillad till perfektion med örter.", true, "Grillad Kyckling", 149m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MenuItems");
        }
    }
}
