using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Migrations
{
    public partial class dbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsVariations_ProductGenders_Gender",
                table: "ProductsVariations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsVariations_Gender",
                table: "ProductsVariations");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("837707c8-7d9d-4274-9bdc-1168cfb4eacb"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("de9dc45e-ad66-404b-9e4c-501ac441af4f"));

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ProductsVariations");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations",
                columns: new[] { "ProductId", "Color", "Size" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: "Male");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: "Male");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Gender",
                value: "Male");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("928fc5cc-1e5a-408e-ad01-26edb6cfc37c"), "CODE10", 10, true },
                    { new Guid("e1f312da-b611-4924-99bf-c5b67e6f3dea"), "CODE20", 20, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Gender",
                table: "Products",
                column: "Gender");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGenders_Gender",
                table: "Products",
                column: "Gender",
                principalTable: "ProductGenders",
                principalColumn: "Gender",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGenders_Gender",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations");

            migrationBuilder.DropIndex(
                name: "IX_Products_Gender",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "White", 1, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "White", 1, "S" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "White", 2, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "White", 2, "S" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "Black", 3, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "ProductId", "Size" },
                keyValues: new object[] { "Black", 3, "S" });

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("928fc5cc-1e5a-408e-ad01-26edb6cfc37c"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("e1f312da-b611-4924-99bf-c5b67e6f3dea"));

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "ProductsVariations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsVariations",
                table: "ProductsVariations",
                columns: new[] { "ProductId", "Color", "Size", "Gender" });

            migrationBuilder.InsertData(
                table: "ProductsVariations",
                columns: new[] { "Color", "Gender", "ProductId", "Size", "Quantity" },
                values: new object[,]
                {
                    { "White", "Male", 1, "M", 10 },
                    { "White", "Male", 1, "S", 10 },
                    { "White", "Male", 2, "M", 10 },
                    { "White", "Male", 2, "S", 10 },
                    { "Black", "Male", 3, "M", 10 },
                    { "Black", "Male", 3, "S", 10 }
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("837707c8-7d9d-4274-9bdc-1168cfb4eacb"), "CODE10", 10, true },
                    { new Guid("de9dc45e-ad66-404b-9e4c-501ac441af4f"), "CODE20", 20, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsVariations_Gender",
                table: "ProductsVariations",
                column: "Gender");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsVariations_ProductGenders_Gender",
                table: "ProductsVariations",
                column: "Gender",
                principalTable: "ProductGenders",
                principalColumn: "Gender",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
