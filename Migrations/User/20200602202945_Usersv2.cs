using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Table.Migrations.User
{
    public partial class Usersv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateFirstToken",
                table: "Useres",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateSecondToken",
                table: "Useres",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateFirstToken",
                table: "Useres");

            migrationBuilder.DropColumn(
                name: "dateSecondToken",
                table: "Useres");
        }
    }
}
