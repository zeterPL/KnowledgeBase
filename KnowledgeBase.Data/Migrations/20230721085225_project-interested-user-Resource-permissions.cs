using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectinteresteduserResourcepermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("809d55dc-266c-4171-adc5-81dd92d4d23a"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f09c9f85-217a-450d-be6a-21cdbdae4621"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("fd728481-ecbd-45f4-9ad2-03698c1bcb4c"));

            migrationBuilder.CreateTable(
                name: "ProjectInterestedUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "UserResourcePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResourcePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserResourcePermission_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResourcePermission_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("391bd4e4-7fac-485b-b716-cb9659a106b8"), "Admin user role", "Admin" },
                    { new Guid("c3a3470a-bfa0-43b5-a960-6fba42901c42"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("d1065a82-edb7-4f0c-ba60-f7414c74a5ae"), "Basic user role", "Basic" }
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
                name: "IX_UserResourcePermission_ResourceId",
                table: "UserResourcePermission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResourcePermission_UserId",
                table: "UserResourcePermission",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectInterestedUser");

            migrationBuilder.DropTable(
                name: "UserResourcePermission");

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

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("07e5c474-1395-4b16-840b-58b5a69fe53d"), "Basic user role", "Basic" },
                    { new Guid("3632deb5-0642-4d30-b5d8-0a21028e1d1a"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("ddd1b4a9-7db5-463d-af45-a166d97d9ed0"), "Admin user role", "Admin" }
                });
        }
    }
}
