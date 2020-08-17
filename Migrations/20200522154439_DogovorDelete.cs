using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations
{
    public partial class DogovorDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");
        }


    }
}
