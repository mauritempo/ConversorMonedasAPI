using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addcurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "currencyConversions",
                columns: new[] { "Id", "Code", "ConvertibilityIndex", "Legend", "Status", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 1, "ARS", 0.002m, "Peso Argentino", 0, "$", null },
                    { 2, "EUR", 1.09m, "Euro", 0, "€", null },
                    { 3, "KC", 0.043m, "Corona Checa", 0, "Kč", null },
                    { 4, "USD", 1m, "Dólar Americano", 0, "$", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "currencyConversions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "currencyConversions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "currencyConversions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "currencyConversions",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
