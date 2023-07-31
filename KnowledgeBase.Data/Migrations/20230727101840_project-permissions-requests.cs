using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectpermissionsrequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserProjectPermission_ProjectId",
                table: "UserProjectPermission");

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
                name: "Id",
                table: "UserProjectPermission");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionName",
                table: "UserProjectPermission",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Project",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(2023, 7, 27, 12, 18, 40, 339, DateTimeKind.Local).AddTicks(8668),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Project",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission",
                columns: new[] { "ProjectId", "UserId", "PermissionName" });

            migrationBuilder.CreateTable(
                name: "ProjectPermissionRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedPermission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeRequested = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPermissionRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPermissionRequest_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectPermissionRequest_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectPermissionRequest_Project_ProjectId",
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
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Basic user role", "Basic" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Admin user role", "Admin" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "SuperAdmin user role", "SuperAdmin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_OwnerId",
                table: "Project",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPermissionRequest_ProjectId",
                table: "ProjectPermissionRequest",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPermissionRequest_ReceiverId",
                table: "ProjectPermissionRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPermissionRequest_SenderId",
                table: "ProjectPermissionRequest",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_OwnerId",
                table: "Project",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_OwnerId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ProjectPermissionRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission");

            migrationBuilder.DropIndex(
                name: "IX_Project_OwnerId",
                table: "Project");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"));

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionName",
                table: "UserProjectPermission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserProjectPermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Project",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldDefaultValue: new DateTime(2023, 7, 27, 12, 18, 40, 339, DateTimeKind.Local).AddTicks(8668));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission",
                column: "Id");

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
                name: "IX_UserProjectPermission_ProjectId",
                table: "UserProjectPermission",
                column: "ProjectId");
        }
    }
}
