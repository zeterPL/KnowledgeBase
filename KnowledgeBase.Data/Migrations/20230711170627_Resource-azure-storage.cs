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

            migrationBuilder.AddColumn<string>(
                name: "AzureFileName",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AzureStorageAbsolutePath",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7fe21f18-00e3-44f4-86e2-fc9c9a0fcf6c"), "6f68b8ea-8caf-4a12-94a6-89727246b7cf", "Admin user role", "Admin", null },
                    { new Guid("94daf613-8a90-4b83-8caa-8cb20679d639"), "cb6708d9-28cb-4a23-b43c-54ab0d0c78e7", "Basic user role", "Basic", null },
                    { new Guid("d116202b-5004-4a0b-97a0-591f490bd408"), "a75d4e34-8304-45a6-9eb3-c59a76b0b27d", "SuperAdmin user role", "SuperAdmin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7fe21f18-00e3-44f4-86e2-fc9c9a0fcf6c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("94daf613-8a90-4b83-8caa-8cb20679d639"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d116202b-5004-4a0b-97a0-591f490bd408"));

            migrationBuilder.DropColumn(
                name: "AzureFileName",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "AzureStorageAbsolutePath",
                table: "Resource");

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
    }
}
