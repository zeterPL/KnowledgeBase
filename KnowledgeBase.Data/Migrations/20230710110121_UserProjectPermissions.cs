using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProjectPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0277b0d2-8b1c-473d-b25f-a986249f38cc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4ea0ee7e-2f95-4033-832b-1c11cac90312"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98ca31e8-56d4-461a-84b7-121ab4d0fc27"));

            migrationBuilder.CreateTable(
                name: "UserProjectPermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProjectPermission_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectPermission_Project_ProjectId",
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
                    { new Guid("3e5260c4-d258-401d-b5e8-9d876d84c88e"), "0fbda9a8-6537-4fc2-adef-6168e9c5a749", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("7f98b34e-865c-42fe-8947-1316c1d94d62"), "00c274da-002b-4642-9be6-1d7f91d148ea", "Admin user role", "Admin", null },
                    { new Guid("b28e1435-ea3e-467a-8a02-a1e60a2a25de"), "416ba37a-ad33-4e00-a833-897024026f7d", "Basic user role", "Basic", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_ProjectId",
                table: "UserProjectPermission",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_UserId",
                table: "UserProjectPermission",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectPermission");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e5260c4-d258-401d-b5e8-9d876d84c88e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f98b34e-865c-42fe-8947-1316c1d94d62"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b28e1435-ea3e-467a-8a02-a1e60a2a25de"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0277b0d2-8b1c-473d-b25f-a986249f38cc"), "4b538241-b69c-40e6-9b53-5b1fb155c854", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("4ea0ee7e-2f95-4033-832b-1c11cac90312"), "21725480-35a7-466d-a8d1-5b76162d5bdd", "Admin user role", "Admin", null },
                    { new Guid("98ca31e8-56d4-461a-84b7-121ab4d0fc27"), "19bc532e-4e86-43c7-ad04-233c2970e779", "Basic user role", "Basic", null }
                });
        }
    }
}
