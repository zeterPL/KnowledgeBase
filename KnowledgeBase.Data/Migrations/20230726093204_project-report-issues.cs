using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectreportissues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("391bd4e4-7fac-485b-b716-cb9659a106b8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c3a3470a-bfa0-43b5-a960-6fba42901c42"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d1065a82-edb7-4f0c-ba60-f7414c74a5ae"));

            migrationBuilder.CreateTable(
                name: "ReportProjectIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    IssueType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportProjectIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportProjectIssue_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportProjectIssue_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("6f73ba92-619c-4b77-a44d-c5d671752d6d"), "Basic user role", "Basic" },
                    { new Guid("7eaf9a8c-68b2-4698-aca3-d312d1590adc"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("dd0c0014-d81d-4f45-b05b-890b78a71220"), "Admin user role", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportProjectIssue_ProjectId",
                table: "ReportProjectIssue",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProjectIssue_UserId",
                table: "ReportProjectIssue",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportProjectIssue");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6f73ba92-619c-4b77-a44d-c5d671752d6d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("7eaf9a8c-68b2-4698-aca3-d312d1590adc"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dd0c0014-d81d-4f45-b05b-890b78a71220"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Project");

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"), false, "Deafult project" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("391bd4e4-7fac-485b-b716-cb9659a106b8"), "Admin user role", "Admin" },
                    { new Guid("c3a3470a-bfa0-43b5-a960-6fba42901c42"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("d1065a82-edb7-4f0c-ba60-f7414c74a5ae"), "Basic user role", "Basic" }
                });
        }
    }
}
