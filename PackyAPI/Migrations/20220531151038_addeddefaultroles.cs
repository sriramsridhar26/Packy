using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackyAPI.Migrations
{
    public partial class addeddefaultroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15a0f77c-05f2-468f-b2c7-a93b00e8893e", "03772a4f-95cc-4d5d-b983-0095974b36a8", "Admin", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b67aa976-4baf-454e-9e09-b79881033556", "eff303c8-9f63-4fb1-a5d1-ae7319a98d6c", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15a0f77c-05f2-468f-b2c7-a93b00e8893e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b67aa976-4baf-454e-9e09-b79881033556");
        }
    }
}
