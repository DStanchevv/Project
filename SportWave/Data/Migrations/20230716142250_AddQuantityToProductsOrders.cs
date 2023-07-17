using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class AddQuantityToProductsOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("1070a268-442b-44ab-a3b4-db97a0dd89d8"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("72b49963-f8f8-47d0-9de5-25dec2a90acd"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductsOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("20e07d66-2579-4a23-a721-104c29e1950a"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("69b086f0-8576-49df-b168-28b973a64f3c"), "CODE20", 20, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("20e07d66-2579-4a23-a721-104c29e1950a"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("69b086f0-8576-49df-b168-28b973a64f3c"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductsOrders");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("1070a268-442b-44ab-a3b4-db97a0dd89d8"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("72b49963-f8f8-47d0-9de5-25dec2a90acd"), "CODE20", 20, true });
        }
    }
}
