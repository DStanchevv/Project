using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class ChangeUserPaymentMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("120906e5-de6e-4ede-b2d3-2f72ffc023eb"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("93be0241-5d0a-4b79-987c-95e068e7c8a2"));

            migrationBuilder.DropColumn(
                name: "isDefault",
                table: "UsersPaymentMethods");

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("81559b53-022a-4402-9154-b65097aa93fa"), "CODE10", 10, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("c9017b78-1b18-46fa-812f-66e373f6ea4e"), "CODE20", 20, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("81559b53-022a-4402-9154-b65097aa93fa"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("c9017b78-1b18-46fa-812f-66e373f6ea4e"));

            migrationBuilder.AddColumn<bool>(
                name: "isDefault",
                table: "UsersPaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("120906e5-de6e-4ede-b2d3-2f72ffc023eb"), "CODE20", 20, true });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "Value", "isValid" },
                values: new object[] { new Guid("93be0241-5d0a-4b79-987c-95e068e7c8a2"), "CODE10", 10, true });
        }
    }
}
