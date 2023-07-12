using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class asuserroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aed7d311-f04f-4103-8cc9-8d92f16907fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d53274a1-ad9a-45e8-9022-d8b8e67a13df"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fdb4de25-6bb7-4118-a1b2-ec2882457109"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2e4aad75-a51e-4fb7-8061-f4b98f7c61fc"), "be689ce3-4fd7-4531-888f-6458ae8a8662", "Admin user role", "Admin", null },
                    { new Guid("565a5e47-d75c-4e8d-a8a1-b75e21a60c0d"), "373000eb-1cfa-48a6-85df-32ae0939956b", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("868773a4-1051-44ae-9744-97bae50614c8"), "273eb104-9c38-4ce4-96bc-08c40b0bbe6f", "Basic user role", "Basic", null }
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });


            migrationBuilder.CreateIndex(
              name: "IX_AspNetUserRoles_RoleId",
              table: "AspNetUserRoles",
              column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e4aad75-a51e-4fb7-8061-f4b98f7c61fc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("565a5e47-d75c-4e8d-a8a1-b75e21a60c0d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("868773a4-1051-44ae-9744-97bae50614c8"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("aed7d311-f04f-4103-8cc9-8d92f16907fe"), "e1376204-a7a5-489f-8f8a-83d5ec88c1a7", "Admin user role", "Admin", null },
                    { new Guid("d53274a1-ad9a-45e8-9022-d8b8e67a13df"), "6b2b5cb5-52ac-4808-abbd-191d73b940b9", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("fdb4de25-6bb7-4118-a1b2-ec2882457109"), "cf98d0c2-d3b6-4049-a93b-3103d4b37b8f", "Basic user role", "Basic", null }
                });
        }
    }
}
