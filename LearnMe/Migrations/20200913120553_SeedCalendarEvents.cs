using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Migrations
{
    public partial class SeedCalendarEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CalendarEvents",
                columns: new[] { "Id", "Description", "End", "IsDone", "Start", "Title" },
                values: new object[] { 1, "Animals vocabulary", new DateTime(2020, 9, 22, 16, 5, 51, 649, DateTimeKind.Utc).AddTicks(2156), false, new DateTime(2020, 9, 22, 14, 5, 51, 649, DateTimeKind.Utc).AddTicks(1304), "A1 lesson" });

            migrationBuilder.InsertData(
                table: "CalendarEvents",
                columns: new[] { "Id", "Description", "End", "IsDone", "Start", "Title" },
                values: new object[] { 2, "Future simple", new DateTime(2020, 9, 4, 16, 5, 51, 649, DateTimeKind.Utc).AddTicks(3175), true, new DateTime(2020, 9, 4, 14, 5, 51, 649, DateTimeKind.Utc).AddTicks(3149), "C1 lesson" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
