using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class ChangeCartItemsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("5965f022-e03a-4695-9234-abc815706437"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("fde50ca8-ed6f-4ac7-8936-119152fd1972"));

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ShoppingCartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                columns: new[] { "CartId", "ProductId", "Size" });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("120906e5-de6e-4ede-b2d3-2f72ffc023eb"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("93be0241-5d0a-4b79-987c-95e068e7c8a2"), "CODE10", 10, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("120906e5-de6e-4ede-b2d3-2f72ffc023eb"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("93be0241-5d0a-4b79-987c-95e068e7c8a2"));

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("5965f022-e03a-4695-9234-abc815706437"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("fde50ca8-ed6f-4ac7-8936-119152fd1972"), "CODE20", 20, true });
        }
    }
}
