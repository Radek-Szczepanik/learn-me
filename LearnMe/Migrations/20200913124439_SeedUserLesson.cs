using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Migrations
{
    public partial class SeedUserLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CalendarEventId", "LessonStatus", "MessageText", "RelatedInvoiceId", "Title" },
                values: new object[,] {
                    { 2, 2, 0, "Please come", null, "Lesson 1" },
                    { 3, 8, 0, "Please come", null, "Lesson 1" },
                    { 4, 4, 0, "Please come", null, "Lesson 1" }
                });

            migrationBuilder.InsertData(
                table: "UserLessons",
                columns: new[] { "Id", "LessonId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 4, 3, 3 },
                    { 3, 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserLessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserLessons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserLessons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserLessons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
