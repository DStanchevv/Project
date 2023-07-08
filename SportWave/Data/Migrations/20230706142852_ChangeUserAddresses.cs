using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class ChangeUserAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("81559b53-022a-4402-9154-b65097aa93fa"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("c9017b78-1b18-46fa-812f-66e373f6ea4e"));

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "UsersAddresses");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("0fd7f6bd-a6cb-4f87-b84a-c8ad20b93605"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("ac234074-8523-4e30-8774-661b227e3ece"), "CODE10", 10, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("0fd7f6bd-a6cb-4f87-b84a-c8ad20b93605"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("ac234074-8523-4e30-8774-661b227e3ece"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "UsersAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("81559b53-022a-4402-9154-b65097aa93fa"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("c9017b78-1b18-46fa-812f-66e373f6ea4e"), "CODE20", 20, true });
        }
    }
}
