using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class dbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsVariations_ProductColors_Color",
                table: "ProductsVariations");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsVariations_Color",
                table: "ProductsVariations");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("57c882a2-ffd3-42fd-a22d-d64037ca951b"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("f0a7a067-2f4f-4f9a-91d7-ceebdcfd9b64"));

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductsVariations");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations",
                columns: new[] { "ProductId", "SizeId" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Color",
                value: "WHite");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Color",
                value: "White");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "Blue");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("d1418491-4822-4d20-8ffa-973e41f71a80"), "CODE10", 10, true },
                    { new Guid("e93f3b6f-3b14-45eb-a21b-d7e6ce1d33bc"), "CODE20", 20, true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations");

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("d1418491-4822-4d20-8ffa-973e41f71a80"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("e93f3b6f-3b14-45eb-a21b-d7e6ce1d33bc"));

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductsVariations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations",
                columns: new[] { "ProductId", "Color", "SizeId" });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    Color = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.Color);
                });

            migrationBuilder.InsertData(
                table: "ProductColors",
                column: "Color",
                values: new object[]
                {
                    "Black",
                    "Red",
                    "White"
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("57c882a2-ffd3-42fd-a22d-d64037ca951b"), "CODE10", 10, true },
                    { new Guid("f0a7a067-2f4f-4f9a-91d7-ceebdcfd9b64"), "CODE20", 20, true }
                });

            migrationBuilder.InsertData(
                table: "ProductsVariations",
                columns: new[] { "Color", "ProductId", "SizeId", "Quantity" },
                values: new object[,]
                {
                    { "White", 1, 1, 10 },
                    { "White", 1, 2, 10 },
                    { "White", 2, 1, 10 },
                    { "White", 2, 2, 10 },
                    { "Black", 3, 1, 10 },
                    { "Black", 3, 2, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsVariations_Color",
                table: "ProductsVariations",
                column: "Color");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsVariations_ProductColors_Color",
                table: "ProductsVariations",
                column: "Color",
                principalTable: "ProductColors",
                principalColumn: "Color",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
