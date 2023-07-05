using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class addSizeToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("8adfeb01-9dcb-43ac-8e19-caf2e49709e2"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("a4c2ad73-1d79-4881-bf65-102073452f84"));

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("5965f022-e03a-4695-9234-abc815706437"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("fde50ca8-ed6f-4ac7-8936-119152fd1972"), "CODE20", 20, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("5965f022-e03a-4695-9234-abc815706437"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("fde50ca8-ed6f-4ac7-8936-119152fd1972"));

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShoppingCartItems");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("8adfeb01-9dcb-43ac-8e19-caf2e49709e2"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("a4c2ad73-1d79-4881-bf65-102073452f84"), "CODE20", 20, true });
        }
    }
}
