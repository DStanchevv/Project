﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class changeProductsOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("0fd7f6bd-a6cb-4f87-b84a-c8ad20b93605"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("ac234074-8523-4e30-8774-661b227e3ece"));

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductsOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("4810e9f4-2711-49cf-a52a-e7163ae9eed6"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("d80e9924-4431-47e2-9fef-4238cab36466"), "CODE10", 10, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("4810e9f4-2711-49cf-a52a-e7163ae9eed6"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("d80e9924-4431-47e2-9fef-4238cab36466"));

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductsOrders");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("0fd7f6bd-a6cb-4f87-b84a-c8ad20b93605"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("ac234074-8523-4e30-8774-661b227e3ece"), "CODE10", 10, true });
        }
    }
}
