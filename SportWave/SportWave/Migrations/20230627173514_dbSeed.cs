using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Migrations
{
    public partial class dbSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatuses",
                column: "Status",
                values: new object[]
                {
                    "On the way",
                    "Shipped"
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Card" },
                    { 2, "Cash" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "T-Shirts" },
                    { 2, "Hoodies" },
                    { 3, "Shorts" }
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
                table: "ProductGenders",
                column: "Gender",
                values: new object[]
                {
                    "Female",
                    "Male"
                });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                column: "Size",
                values: new object[]
                {
                    "L",
                    "M",
                    "S",
                    "XL",
                    "XS"
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[,]
                {
                    { new Guid("837707c8-7d9d-4274-9bdc-1168cfb4eacb"), "CODE10", 10, true },
                    { new Guid("de9dc45e-ad66-404b-9e4c-501ac441af4f"), "CODE20", 20, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUrl", "Name", "Price" },
                values: new object[] { 1, 1, "A very light, soft and comfortable T-shirt made of 100% cotton.", "/img/T-Shirt V1.jpg", "T-Shirt V1", 15.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUrl", "Name", "Price" },
                values: new object[] { 2, 2, "A very light, soft and comfortable hoodie made of 100% cotton.", "/img/Hoodie V1.jpg", "Hoodie V1", 20.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUrl", "Name", "Price" },
                values: new object[] { 3, 3, "A very light, soft and comfortable Shorts made of 100% cotton.", "/img/Shorts V1.jpg", "Shorts V1", 20.99m });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Status",
                keyValue: "On the way");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Status",
                keyValue: "Shipped");

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Color",
                keyValue: "Red");

            migrationBuilder.DeleteData(
                table: "ProductGenders",
                keyColumn: "Gender",
                keyValue: "Female");

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Size",
                keyValue: "L");

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Size",
                keyValue: "XL");

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Size",
                keyValue: "XS");

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "White", "Male", 1, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "White", "Male", 1, "S" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "White", "Male", 2, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "White", "Male", 2, "S" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "Black", "Male", 3, "M" });

            migrationBuilder.DeleteData(
                table: "ProductsVariations",
                keyColumns: new[] { "Color", "Gender", "ProductId", "Size" },
                keyValues: new object[] { "Black", "Male", 3, "S" });

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("837707c8-7d9d-4274-9bdc-1168cfb4eacb"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("de9dc45e-ad66-404b-9e4c-501ac441af4f"));

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Color",
                keyValue: "Black");

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Color",
                keyValue: "White");

            migrationBuilder.DeleteData(
                table: "ProductGenders",
                keyColumn: "Gender",
                keyValue: "Male");

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Size",
                keyValue: "M");

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Size",
                keyValue: "S");

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

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
