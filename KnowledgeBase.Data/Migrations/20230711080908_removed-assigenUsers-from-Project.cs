using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedassigenUsersfromProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProject");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e33ab6c-efea-4f62-bdf2-e55e741389c9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a625259-e4e7-4409-9930-7bb58ee22eaf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d447125f-b29a-4cd6-b1db-969223021451"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("20c54f30-178b-4cdd-889a-db758199b1b7"), "12c6085b-37d2-4a54-88e3-f564e9b4cfbb", "Basic user role", "Basic", null },
                    { new Guid("3aa9fa0b-57e3-4c99-9ef0-765b5262b00a"), "728348ab-fe6d-41fa-9652-77adeb06ba27", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("5e792e1d-c74c-43e3-9eb2-a31738907169"), "66a11273-cb54-4cbf-b4bc-1a5f6f35131c", "Admin user role", "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("20c54f30-178b-4cdd-889a-db758199b1b7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3aa9fa0b-57e3-4c99-9ef0-765b5262b00a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e792e1d-c74c-43e3-9eb2-a31738907169"));

            migrationBuilder.CreateTable(
                name: "UserProject",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProject_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2e33ab6c-efea-4f62-bdf2-e55e741389c9"), "4259e199-be62-459a-8d5c-6415ebc74fb1", "Admin user role", "Admin", null },
                    { new Guid("7a625259-e4e7-4409-9930-7bb58ee22eaf"), "c2d27718-4921-4a5e-8599-0eebf393753c", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("d447125f-b29a-4cd6-b1db-969223021451"), "9b2036b4-7d0c-4506-be54-0c5057b4c446", "Basic user role", "Basic", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserId",
                table: "UserProject",
                column: "UserId");
        }
    }
}
