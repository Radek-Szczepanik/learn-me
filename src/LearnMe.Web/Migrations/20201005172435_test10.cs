using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Web.Migrations
{
    public partial class test10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBasics_UserBasic_StudentId",
                table: "InvoiceBasics");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserBasic_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserBasic_ToUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvoiceDatas_UserBasic_UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_UserBasic_UserId",
                table: "UserLessons");

            migrationBuilder.DropTable(
                name: "UserBasic");

            migrationBuilder.DropIndex(
                name: "IX_UserLessons_UserId",
                table: "UserLessons");

            migrationBuilder.DropIndex(
                name: "IX_UserInvoiceDatas_UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceBasics_StudentId",
                table: "InvoiceBasics");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserLessons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromUserId1",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserId1",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId1",
                table: "InvoiceBasics",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserGroupId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLessons_UserId1",
                table: "UserLessons",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId1",
                table: "Messages",
                column: "FromUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserId1",
                table: "Messages",
                column: "ToUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBasics_StudentId1",
                table: "InvoiceBasics",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserGroupId",
                table: "AspNetUsers",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserGroups_UserGroupId",
                table: "AspNetUsers",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBasics_AspNetUsers_StudentId1",
                table: "InvoiceBasics",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_FromUserId1",
                table: "Messages",
                column: "FromUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ToUserId1",
                table: "Messages",
                column: "ToUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessons_AspNetUsers_UserId1",
                table: "UserLessons",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserGroups_UserGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBasics_AspNetUsers_StudentId1",
                table: "InvoiceBasics");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_FromUserId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ToUserId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_AspNetUsers_UserId1",
                table: "UserLessons");

            migrationBuilder.DropIndex(
                name: "IX_UserLessons_UserId1",
                table: "UserLessons");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromUserId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ToUserId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceBasics_StudentId1",
                table: "InvoiceBasics");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserLessons");

            migrationBuilder.DropColumn(
                name: "FromUserId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ToUserId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "InvoiceBasics");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserInvoiceDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserBasic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Postcode = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBasic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBasic_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLessons_UserId",
                table: "UserLessons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvoiceDatas_UserId",
                table: "UserInvoiceDatas",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBasics_StudentId",
                table: "InvoiceBasics",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBasic_UserGroupId",
                table: "UserBasic",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBasics_UserBasic_StudentId",
                table: "InvoiceBasics",
                column: "StudentId",
                principalTable: "UserBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserBasic_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "UserBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserBasic_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "UserBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvoiceDatas_UserBasic_UserId",
                table: "UserInvoiceDatas",
                column: "UserId",
                principalTable: "UserBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessons_UserBasic_UserId",
                table: "UserLessons",
                column: "UserId",
                principalTable: "UserBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
