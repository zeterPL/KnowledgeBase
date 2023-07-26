using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class userprojectpermissionkey : Migration
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission",
                columns: new[] { "ProjectId", "UserId", "PermissionName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjectPermission",
                table: "UserProjectPermission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_ProjectId",
                table: "UserProjectPermission",
                column: "ProjectId");
        }
    }
}
