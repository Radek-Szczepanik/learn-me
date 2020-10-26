using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMe.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CalendarEvents",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        Start = table.Column<DateTime>(nullable: true),
            //        End = table.Column<DateTime>(nullable: true),
            //        IsDone = table.Column<bool>(nullable: false),
            //        CalendarId = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CalendarEvents", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Corrections",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FileString = table.Column<string>(nullable: false),
            //        Feedback = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Corrections", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Exercises",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        FileString = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(nullable: false),
            //        ExerciseGroup = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Exercises", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "News",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        FileString = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_News", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Opinions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        FileString = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(nullable: false),
            //        Rating = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Opinions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Portfolios",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        FileString = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Portfolios", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Products",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Products", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Questions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionText = table.Column<string>(nullable: false),
            //        AnswerText = table.Column<string>(nullable: false),
            //        KeywordOrGroupIdentifier = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Questions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TutorServices",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        FileString = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(nullable: false),
            //        Type = table.Column<int>(nullable: false),
            //        IsAvailable = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TutorServices", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserGroups",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Email = table.Column<string>(nullable: false),
            //        Password = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        OptionalDesription = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserGroups", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserInvoiceDatas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserInvoiceDatas", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(nullable: false),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        UserName = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
            //        Email = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(nullable: false),
            //        PasswordHash = table.Column<string>(nullable: true),
            //        SecurityStamp = table.Column<string>(nullable: true),
            //        ConcurrencyStamp = table.Column<string>(nullable: true),
            //        PhoneNumber = table.Column<string>(nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
            //        LockoutEnabled = table.Column<bool>(nullable: false),
            //        AccessFailedCount = table.Column<int>(nullable: false),
            //        FirstName = table.Column<string>(nullable: false),
            //        LastName = table.Column<string>(nullable: false),
            //        Status = table.Column<int>(nullable: false),
            //        UserGroupId = table.Column<int>(nullable: true),
            //        Notes = table.Column<string>(nullable: true),
            //        InvoiceDataId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUsers_UserInvoiceDatas_InvoiceDataId",
            //            column: x => x.InvoiceDataId,
            //            principalTable: "UserInvoiceDatas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_AspNetUsers_UserGroups_UserGroupId",
            //            column: x => x.UserGroupId,
            //            principalTable: "UserGroups",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(nullable: false),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(nullable: false),
            //        ProviderKey = table.Column<string>(nullable: false),
            //        ProviderDisplayName = table.Column<string>(nullable: true),
            //        UserId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        RoleId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        LoginProvider = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "InvoiceBasics",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Email = table.Column<string>(nullable: false),
            //        Password = table.Column<string>(nullable: false),
            //        StudentId = table.Column<int>(nullable: false),
            //        NumberOfHours = table.Column<int>(nullable: false),
            //        SumToPayInPLN = table.Column<int>(nullable: false),
            //        PaymentStatus = table.Column<int>(nullable: false),
            //        PaymentAction = table.Column<int>(nullable: false),
            //        InvoiceNumber = table.Column<string>(nullable: false),
            //        InvoiceFile = table.Column<string>(nullable: true),
            //        StudentId1 = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InvoiceBasics", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_InvoiceBasics_AspNetUsers_StudentId1",
            //            column: x => x.StudentId1,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Messages",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FromUserId = table.Column<int>(nullable: false),
            //        ToUserId = table.Column<int>(nullable: false),
            //        FromUserId1 = table.Column<string>(nullable: true),
            //        ToUserId1 = table.Column<string>(nullable: true),
            //        Title = table.Column<string>(nullable: false),
            //        MessageText = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Messages", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Messages_AspNetUsers_FromUserId1",
            //            column: x => x.FromUserId1,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Messages_AspNetUsers_ToUserId1",
            //            column: x => x.ToUserId1,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Lessons",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        LessonStatus = table.Column<int>(nullable: false),
            //        RelatedInvoiceId = table.Column<int>(nullable: true),
            //        CalendarEventId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Lessons", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Lessons_CalendarEvents_CalendarEventId",
            //            column: x => x.CalendarEventId,
            //            principalTable: "CalendarEvents",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Lessons_InvoiceBasics_RelatedInvoiceId",
            //            column: x => x.RelatedInvoiceId,
            //            principalTable: "InvoiceBasics",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Attachments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FileName = table.Column<string>(nullable: true),
            //        MessageId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Attachments", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Attachments_Messages_MessageId",
            //            column: x => x.MessageId,
            //            principalTable: "Messages",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserLessons",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(nullable: false),
            //        UserId1 = table.Column<string>(nullable: true),
            //        LessonId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserLessons", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserLessons_Lessons_LessonId",
            //            column: x => x.LessonId,
            //            principalTable: "Lessons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserLessons_AspNetUsers_UserId1",
            //            column: x => x.UserId1,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Homeworks",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FileString = table.Column<string>(nullable: false),
            //        MessageText = table.Column<string>(nullable: false),
            //        UserLessonId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Homeworks", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Homeworks_UserLessons_UserLessonId",
            //            column: x => x.UserLessonId,
            //            principalTable: "UserLessons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserLessonHomeworks",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserLessonId = table.Column<int>(nullable: false),
            //        HomeworkId = table.Column<int>(nullable: false),
            //        CorrectionId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserLessonHomeworks", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserLessonHomeworks_Corrections_CorrectionId",
            //            column: x => x.CorrectionId,
            //            principalTable: "Corrections",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserLessonHomeworks_Homeworks_HomeworkId",
            //            column: x => x.HomeworkId,
            //            principalTable: "Homeworks",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserLessonHomeworks_UserLessons_UserLessonId",
            //            column: x => x.UserLessonId,
            //            principalTable: "UserLessons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUsers_InvoiceDataId",
            //    table: "AspNetUsers",
            //    column: "InvoiceDataId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUsers_UserGroupId",
            //    table: "AspNetUsers",
            //    column: "UserGroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Attachments_MessageId",
            //    table: "Attachments",
            //    column: "MessageId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Homeworks_UserLessonId",
            //    table: "Homeworks",
            //    column: "UserLessonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_InvoiceBasics_StudentId1",
            //    table: "InvoiceBasics",
            //    column: "StudentId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lessons_CalendarEventId",
            //    table: "Lessons",
            //    column: "CalendarEventId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lessons_RelatedInvoiceId",
            //    table: "Lessons",
            //    column: "RelatedInvoiceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Messages_FromUserId1",
            //    table: "Messages",
            //    column: "FromUserId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Messages_ToUserId1",
            //    table: "Messages",
            //    column: "ToUserId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLessonHomeworks_CorrectionId",
            //    table: "UserLessonHomeworks",
            //    column: "CorrectionId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLessonHomeworks_HomeworkId",
            //    table: "UserLessonHomeworks",
            //    column: "HomeworkId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLessonHomeworks_UserLessonId",
            //    table: "UserLessonHomeworks",
            //    column: "UserLessonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLessons_LessonId",
            //    table: "UserLessons",
            //    column: "LessonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLessons_UserId1",
            //    table: "UserLessons",
            //    column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "Attachments");

            //migrationBuilder.DropTable(
            //    name: "Exercises");

            //migrationBuilder.DropTable(
            //    name: "News");

            //migrationBuilder.DropTable(
            //    name: "Opinions");

            //migrationBuilder.DropTable(
            //    name: "Portfolios");

            //migrationBuilder.DropTable(
            //    name: "Products");

            //migrationBuilder.DropTable(
            //    name: "Questions");

            //migrationBuilder.DropTable(
            //    name: "TutorServices");

            //migrationBuilder.DropTable(
            //    name: "UserLessonHomeworks");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "Messages");

            //migrationBuilder.DropTable(
            //    name: "Corrections");

            //migrationBuilder.DropTable(
            //    name: "Homeworks");

            //migrationBuilder.DropTable(
            //    name: "UserLessons");

            //migrationBuilder.DropTable(
            //    name: "Lessons");

            //migrationBuilder.DropTable(
            //    name: "CalendarEvents");

            //migrationBuilder.DropTable(
            //    name: "InvoiceBasics");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "UserInvoiceDatas");

            //migrationBuilder.DropTable(
            //    name: "UserGroups");
        }
    }
}
