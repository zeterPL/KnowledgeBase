using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class validpermissionprimarykey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ProjectId",
                table: "Permission",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Permission_ProjectId",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Permission");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                columns: new[] { "ProjectId", "UserId" });
        }
    }
}
