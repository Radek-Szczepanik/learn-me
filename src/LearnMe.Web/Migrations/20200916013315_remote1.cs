using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Migrations
{
    public partial class remote1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalendarEventId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CalendarEventId",
                table: "Lessons",
                column: "CalendarEventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_CalendarEvents_CalendarEventId",
                table: "Lessons",
                column: "CalendarEventId",
                principalTable: "CalendarEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_CalendarEvents_CalendarEventId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CalendarEventId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CalendarEventId",
                table: "Lessons");
        }
    }
}
