using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class removeUnnecessaryFromUserPaymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "UsersPaymentMethods");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "UsersPaymentMethods");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "UsersPaymentMethods");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "UsersPaymentMethods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "UsersPaymentMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "UsersPaymentMethods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "UsersPaymentMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "UsersPaymentMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Card");

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Cash" });
        }
    }
}
