using Microsoft.EntityFrameworkCore.Migrations;

namespace ddaproj.Data.Migrations
{
    public partial class AddedCustomClaimTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomClaims", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CustomClaims",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "Board Member", "BoardMember" });

            migrationBuilder.InsertData(
                table: "CustomClaims",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 2, "Business Member", "BusinessMember" });

            migrationBuilder.InsertData(
                table: "CustomClaims",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 3, "General Member", "GeneralMember" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomClaims");
        }
    }
}
