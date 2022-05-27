using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackyAPI.Migrations
{
    public partial class seedingdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "shortname" },
                values: new object[,]
                {
                    { 1, "Quebec", "QC" },
                    { 2, "Ontario", "ON" },
                    { 3, "New Brunswick", "NB" },
                    { 4, "British Columbia", "BC" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "QCb", 1, "The Quebec Hotel", 4.5 },
                    { 2, "ONt", 2, "The Ontario Hotel", 4.2000000000000002 },
                    { 3, "NBs", 3, "The Brunswick Hotel", 4.0 },
                    { 4, "BCo", 4, "The Vancouver Hotel", 4.9000000000000004 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
