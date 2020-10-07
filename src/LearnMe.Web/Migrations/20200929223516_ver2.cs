using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Web.Migrations
{
    public partial class ver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Start",
                table: "CalendarEvents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "CalendarEvents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "CalendarEvents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start",
                table: "CalendarEvents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "CalendarEvents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
