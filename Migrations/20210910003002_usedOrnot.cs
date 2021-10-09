using Microsoft.EntityFrameworkCore.Migrations;

namespace CLED.Migrations
{
    public partial class usedOrnot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "usedOrnot",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usedOrnot",
                table: "AspNetUsers");
        }
    }
}
