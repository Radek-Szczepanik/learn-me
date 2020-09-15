using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Notes", "Password", "PhoneNumber", "Postcode", "RegistrationDate", "Role", "Status", "UserGroupId" },
                values: new object[,]
                {
                    { 1, "Torun", "maciek@gmail.com", "Maciek", "Kowalski", null, "somePsw", 0, 0, new DateTime(2020, 9, 13, 12, 31, 32, 455, DateTimeKind.Utc).AddTicks(4163), 0, 0, null },
                    { 2, "Torun", "ania@gmail.com", "Anna", "Nowak", null, "somePsw", 0, 0, new DateTime(2020, 9, 13, 12, 34, 32, 455, DateTimeKind.Utc).AddTicks(5207), 0, 0, null },
                    { 3, "Warszawa", "maciek@gmail.com", "Barbara", "Zamojska", null, "somePsw", 0, 0, new DateTime(2020, 9, 13, 14, 31, 32, 455, DateTimeKind.Utc).AddTicks(5284), 0, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
