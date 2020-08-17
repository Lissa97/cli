using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations
{
    public partial class Groupv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_teacher",
                table: "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_teacher",
                table: "Groups");
        }
    }
}
