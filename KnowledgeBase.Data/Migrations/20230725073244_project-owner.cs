using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectowner : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Project",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0a005301-0d16-436e-a38a-f1eeb5097a0f"), "Admin user role", "Admin" },
                    { new Guid("d261a088-45a3-4cb3-b9b4-ec0982a335c9"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("ecc3fda9-c37a-40f2-91a7-c4c79b749843"), "Basic user role", "Basic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_OwnerId",
                table: "Project",
                column: "OwnerId");

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

            migrationBuilder.DropIndex(
                name: "IX_Project_OwnerId",
                table: "Project");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("0a005301-0d16-436e-a38a-f1eeb5097a0f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d261a088-45a3-4cb3-b9b4-ec0982a335c9"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ecc3fda9-c37a-40f2-91a7-c4c79b749843"));

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Project");

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "StartDate" },
                values: new object[] { new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"), null, false, "Deafult project", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
