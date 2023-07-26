using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class addChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "GenderId", "ProductId", "SizeId" },
                keyValues: new object[] { 1, 3, 2 });

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("20e07d66-2579-4a23-a721-104c29e1950a"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("69b086f0-8576-49df-b168-28b973a64f3c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Status",
                keyValue: "Not sent");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Color", "Description", "GenderId", "ImgUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "WHite", "A very light, soft and comfortable T-shirt made of 100% cotton.", 1, "/img/T-Shirt V1.jpg", "T-Shirt V1", 15.99m },
                    { 2, 2, "White", "A very light, soft and comfortable hoodie made of 100% cotton.", 1, "/img/Hoodie V1.jpg", "Hoodie V1", 20.99m },
                    { 3, 3, "Blue", "A very light, soft and comfortable Shorts made of 100% cotton.", 1, "/img/Shorts V1.jpg", "Shorts V1", 20.99m }
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("20e07d66-2579-4a23-a721-104c29e1950a"), "CODE10", 10, true },
                    { new Guid("69b086f0-8576-49df-b168-28b973a64f3c"), "CODE20", 20, true }
                });

            migrationBuilder.InsertData(
                table: "ProductsVariations",
                columns: new[] { "GenderId", "ProductId", "SizeId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 10 },
                    { 1, 1, 2, 10 },
                    { 1, 2, 1, 10 },
                    { 1, 2, 2, 10 },
                    { 1, 3, 1, 10 },
                    { 1, 3, 2, 10 }
                });
        }
    }
}
