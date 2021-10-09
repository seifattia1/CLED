using Microsoft.EntityFrameworkCore.Migrations;

namespace CLED.Migrations
{
    public partial class Facturestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FactureStatus",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FactureStatus",
                table: "Factures");
        }
    }
}
