using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Resourceazurestorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("0a69952f-c9a6-4077-8a36-1ec3cac5aa25"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("18954c46-a9c9-4506-a67d-dc1037fe5236"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6c3b9869-f57c-40bb-adb1-cba576254e44"));

            migrationBuilder.AddColumn<string>(
                name: "AzureFileName",
                table: "Resource",
                type: "nvarchar(104)",
                maxLength: 104,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AzureStorageAbsolutePath",
                table: "Resource",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AzureFileName",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "AzureStorageAbsolutePath",
                table: "Resource");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0a69952f-c9a6-4077-8a36-1ec3cac5aa25"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("18954c46-a9c9-4506-a67d-dc1037fe5236"), "Basic user role", "Basic" },
                    { new Guid("6c3b9869-f57c-40bb-adb1-cba576254e44"), "Admin user role", "Admin" }
                });
        }
    }
}
