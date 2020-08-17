using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations
{
    public partial class RowType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Peoples",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Peoples",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "fathers_name",
                table: "Peoples",
                newName: "Fathers_name");

            migrationBuilder.RenameColumn(
                name: "family_name",
                table: "Peoples",
                newName: "Family_name");

            migrationBuilder.RenameColumn(
                name: "rus_name",
                table: "InfoTables",
                newName: "Rus_name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "InfoTables",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InfoTables",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "RowTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    rus_name = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowTypes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RowTypes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Peoples",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Peoples",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Fathers_name",
                table: "Peoples",
                newName: "fathers_name");

            migrationBuilder.RenameColumn(
                name: "Family_name",
                table: "Peoples",
                newName: "family_name");

            migrationBuilder.RenameColumn(
                name: "Rus_name",
                table: "InfoTables",
                newName: "rus_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "InfoTables",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InfoTables",
                newName: "id");
        }
    }
}
