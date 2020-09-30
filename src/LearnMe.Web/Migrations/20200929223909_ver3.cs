using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Web.Migrations
{
    public partial class ver3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBasics_Users_StudentId",
                table: "InvoiceBasics");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ToUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvoiceDatas_Users_UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_Users_UserId",
                table: "UserLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersBasic");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserGroupId",
                table: "UsersBasic",
                newName: "IX_UsersBasic_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersBasic",
                table: "UsersBasic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBasics_UsersBasic_StudentId",
                table: "InvoiceBasics",
                column: "StudentId",
                principalTable: "UsersBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UsersBasic_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "UsersBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UsersBasic_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "UsersBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvoiceDatas_UsersBasic_UserId",
                table: "UserInvoiceDatas",
                column: "UserId",
                principalTable: "UsersBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessons_UsersBasic_UserId",
                table: "UserLessons",
                column: "UserId",
                principalTable: "UsersBasic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersBasic_UserGroups_UserGroupId",
                table: "UsersBasic",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBasics_UsersBasic_StudentId",
                table: "InvoiceBasics");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UsersBasic_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UsersBasic_ToUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvoiceDatas_UsersBasic_UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_UsersBasic_UserId",
                table: "UserLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersBasic_UserGroups_UserGroupId",
                table: "UsersBasic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersBasic",
                table: "UsersBasic");

            migrationBuilder.RenameTable(
                name: "UsersBasic",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UsersBasic_UserGroupId",
                table: "Users",
                newName: "IX_Users_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBasics_Users_StudentId",
                table: "InvoiceBasics",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvoiceDatas_Users_UserId",
                table: "UserInvoiceDatas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessons_Users_UserId",
                table: "UserLessons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
