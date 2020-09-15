using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Migrations
{
    public partial class SeedLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 9, 22, 16, 11, 36, 268, DateTimeKind.Utc).AddTicks(580), new DateTime(2020, 9, 22, 14, 11, 36, 267, DateTimeKind.Utc).AddTicks(9675) });

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 9, 4, 16, 11, 36, 268, DateTimeKind.Utc).AddTicks(1836), new DateTime(2020, 9, 4, 14, 11, 36, 268, DateTimeKind.Utc).AddTicks(1786) });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CalendarEventId", "LessonStatus", "MessageText", "RelatedInvoiceId", "Title" },
                values: new object[] { 1, 1, 0, "Please come", null, "Lesson 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 9, 22, 16, 5, 51, 649, DateTimeKind.Utc).AddTicks(2156), new DateTime(2020, 9, 22, 14, 5, 51, 649, DateTimeKind.Utc).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 9, 4, 16, 5, 51, 649, DateTimeKind.Utc).AddTicks(3175), new DateTime(2020, 9, 4, 14, 5, 51, 649, DateTimeKind.Utc).AddTicks(3149) });
        }
    }
}
