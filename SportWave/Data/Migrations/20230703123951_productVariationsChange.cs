using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class productVariationsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsVariations_Products_GenderId",
                table: "ProductsVariations");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("8207c71d-8619-4423-8317-73bf1a748bcd"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("f9fd4b96-9613-41bf-b148-788ba1fb5557"));

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("8adfeb01-9dcb-43ac-8e19-caf2e49709e2"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("a4c2ad73-1d79-4881-bf65-102073452f84"), "CODE20", 20, true });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsVariations_ProductGenders_GenderId",
                table: "ProductsVariations",
                column: "GenderId",
                principalTable: "ProductGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsVariations_ProductGenders_GenderId",
                table: "ProductsVariations");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("8adfeb01-9dcb-43ac-8e19-caf2e49709e2"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("a4c2ad73-1d79-4881-bf65-102073452f84"));

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("8207c71d-8619-4423-8317-73bf1a748bcd"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("f9fd4b96-9613-41bf-b148-788ba1fb5557"), "CODE20", 20, true });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsVariations_Products_GenderId",
                table: "ProductsVariations",
                column: "GenderId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
