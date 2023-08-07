using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportWave.Data.Migrations
{
    public partial class addStores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "City", "Country", "Location", "Region" },
                values: new object[] { 1, "Sofia", "Bulgaria", "42.676711, 23.322012", "Sofia" });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "City", "Country", "Location", "Region" },
                values: new object[] { 2, "Plovdiv", "Bulgaria", "42.144982, 24.751170", "Plovdiv" });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "City", "Country", "Location", "Region" },
                values: new object[] { 3, "Stara Zagora", "Bulgaria", "42.428134, 25.626048", "Stara Zagora" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
