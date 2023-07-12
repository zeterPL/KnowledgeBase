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
                type: "nvarchar(104)",
                maxLength: 104,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AzureStorageAbsolutePath",
                table: "Resource",
                type: "nvarchar(205)",
                maxLength: 205,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8277823a-f7fa-4b6b-b0fa-9359d612413c"), "c546af7a-189a-460d-864d-1889b0d0216f", "Admin user role", "Admin", null },
                    { new Guid("8ae473a5-1a54-4259-ae85-ca91ad630fde"), "8a0960b1-c406-42f0-a413-ae823bc48fe8", "Basic user role", "Basic", null },
                    { new Guid("9f0ae49d-9084-48ea-9b51-473f5d116849"), "f2d7b9a6-b6ee-4a93-aece-61d05237e161", "SuperAdmin user role", "SuperAdmin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8277823a-f7fa-4b6b-b0fa-9359d612413c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8ae473a5-1a54-4259-ae85-ca91ad630fde"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f0ae49d-9084-48ea-9b51-473f5d116849"));

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
