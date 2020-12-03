using Microsoft.EntityFrameworkCore.Migrations;

namespace ddaproj.Data.Migrations
{
    public partial class SeededNewClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Value" },
                values: new object[] { "President", "President" });

            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Vice President", "VicePresident" });

            migrationBuilder.InsertData(
                table: "CustomClaims",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 4, "Board Member", "BoardMember" },
                    { 5, "Business Member", "BusinessMember" },
                    { 6, "Individual Member", "IndividualMember" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Board Member", "BoardMember" });

            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Business Member", "BusinessMember" });

            migrationBuilder.UpdateData(
                table: "CustomClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Value" },
                values: new object[] { "General Member", "GeneralMember" });
        }
    }
}
