using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class ChangeProductOrdersPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("4810e9f4-2711-49cf-a52a-e7163ae9eed6"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("d80e9924-4431-47e2-9fef-4238cab36466"));

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductsOrders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders",
                columns: new[] { "ProductId", "OrderId", "Size" });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("41f77e07-b977-4fa6-913b-53b282a70441"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("d9f66b75-7367-489d-9ed7-fb18fccd14cc"), "CODE10", 10, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("41f77e07-b977-4fa6-913b-53b282a70441"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("d9f66b75-7367-489d-9ed7-fb18fccd14cc"));

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductsOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("4810e9f4-2711-49cf-a52a-e7163ae9eed6"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("d80e9924-4431-47e2-9fef-4238cab36466"), "CODE10", 10, true });
        }
    }
}
