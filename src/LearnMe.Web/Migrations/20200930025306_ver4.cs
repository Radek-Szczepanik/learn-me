using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Web.Migrations
{
    public partial class ver4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "UserBasic");

            migrationBuilder.RenameIndex(
                name: "IX_UsersBasic_UserGroupId",
                table: "UserBasic",
                newName: "IX_UserBasic_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBasic",
                table: "UserBasic",
                column: "Id");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBasic_UserGroups_UserGroupId",
                table: "UserBasic",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_UserBasic_UserGroups_UserGroupId",
                table: "UserBasic");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvoiceDatas_UserBasic_UserId",
                table: "UserInvoiceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_UserBasic_UserId",
                table: "UserLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBasic",
                table: "UserBasic");

            migrationBuilder.RenameTable(
                name: "UserBasic",
                newName: "UsersBasic");

            migrationBuilder.RenameIndex(
                name: "IX_UserBasic_UserGroupId",
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
                onDelete: ReferentialAction.Cascade);

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
    }
}
