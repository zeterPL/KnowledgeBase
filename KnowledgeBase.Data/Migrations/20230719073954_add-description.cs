using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectInterestedUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("5b87b3c7-b5c4-40d8-861c-9ecf361e9033"), "Basic user role", "Basic" },
                    { new Guid("87e5094e-7ab5-4e18-b5c9-b4bcdb05b22d"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("acbe6901-c569-49be-999a-5f85ff15a905"), "Admin user role", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("5b87b3c7-b5c4-40d8-861c-9ecf361e9033"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("87e5094e-7ab5-4e18-b5c9-b4bcdb05b22d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("acbe6901-c569-49be-999a-5f85ff15a905"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectInterestedUser");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4d4192d4-59ca-48a3-8f96-8508151ce5b5"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("811a8729-b3bc-4fbe-9b47-52ecabc2d09d"), "Basic user role", "Basic" },
                    { new Guid("b35ebd88-dec8-4ef8-83dc-b74a8b2e4572"), "Admin user role", "Admin" }
                });
        }
    }
}
