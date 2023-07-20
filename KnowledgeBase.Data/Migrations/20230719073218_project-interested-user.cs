using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectinteresteduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6eefdab9-5ddc-4629-b53f-d497da886e0b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("79f89f50-d520-49f1-bbdf-adb13a5f8a96"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("9abfb461-0a83-4094-8e36-171d84aa9d1e"));

            migrationBuilder.CreateTable(
                name: "ProjectInterestedUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInterestedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectInterestedUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectInterestedUser_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => new { x.ProjectId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProjectTag_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4d4192d4-59ca-48a3-8f96-8508151ce5b5"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("811a8729-b3bc-4fbe-9b47-52ecabc2d09d"), "Basic user role", "Basic" },
                    { new Guid("b35ebd88-dec8-4ef8-83dc-b74a8b2e4572"), "Admin user role", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInterestedUser_ProjectId",
                table: "ProjectInterestedUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInterestedUser_UserId",
                table: "ProjectInterestedUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_TagId",
                table: "ProjectTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectInterestedUser");

            migrationBuilder.DropTable(
                name: "ProjectTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("4d4192d4-59ca-48a3-8f96-8508151ce5b5"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("811a8729-b3bc-4fbe-9b47-52ecabc2d09d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b35ebd88-dec8-4ef8-83dc-b74a8b2e4572"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("6eefdab9-5ddc-4629-b53f-d497da886e0b"), "Basic user role", "Basic" },
                    { new Guid("79f89f50-d520-49f1-bbdf-adb13a5f8a96"), "Admin user role", "Admin" },
                    { new Guid("9abfb461-0a83-4094-8e36-171d84aa9d1e"), "SuperAdmin user role", "SuperAdmin" }
                });
        }
    }
}
