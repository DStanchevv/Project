using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class SCTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("bd8a5035-61d2-4c60-8fad-10a13e0131ea"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("c3ebf5cc-eae1-4b33-b689-631230640f5f"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("01606110-678c-4b6d-866a-0310ad5e473e"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("c0408389-166a-4ec0-96b7-7880cc8b629e"), "CODE10", 10, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("01606110-678c-4b6d-866a-0310ad5e473e"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("c0408389-166a-4ec0-96b7-7880cc8b629e"));

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCarts");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("bd8a5035-61d2-4c60-8fad-10a13e0131ea"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("c3ebf5cc-eae1-4b33-b689-631230640f5f"), "CODE10", 10, true });
        }
    }
}
