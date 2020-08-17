using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations
{
    public partial class RowTypev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "RowTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "rus_name",
                table: "RowTypes",
                newName: "Rus_name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RowTypes",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Table_id",
                table: "RowTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Table_id",
                table: "RowTypes");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "RowTypes",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Rus_name",
                table: "RowTypes",
                newName: "rus_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RowTypes",
                newName: "name");
        }
    }
}
