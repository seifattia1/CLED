using Microsoft.EntityFrameworkCore.Migrations;

namespace CLED.Migrations
{
    public partial class country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coutry",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coutry",
                table: "AspNetUsers");
        }
    }
}
