using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class hardcodedroleid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Basic user role", "Basic" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Admin user role", "Admin" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "SuperAdmin user role", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0a005301-0d16-436e-a38a-f1eeb5097a0f"), "Admin user role", "Admin" },
                    { new Guid("d261a088-45a3-4cb3-b9b4-ec0982a335c9"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("ecc3fda9-c37a-40f2-91a7-c4c79b749843"), "Basic user role", "Basic" }
                });
        }
    }
}
