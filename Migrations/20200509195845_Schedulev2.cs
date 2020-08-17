using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations
{
    public partial class Schedulev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group = table.Column<string>(nullable: true),
                    cours = table.Column<string>(nullable: true),
                    teacher = table.Column<string>(nullable: true),
                    id_group = table.Column<int>(nullable: false),
                    id_cours = table.Column<int>(nullable: false),
                    id_teacher = table.Column<int>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    stop = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
