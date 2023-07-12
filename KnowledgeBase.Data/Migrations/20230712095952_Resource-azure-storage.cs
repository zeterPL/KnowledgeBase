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
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("088b414b-944e-484a-81f5-6a675c4c714b"), "3be5c6dc-797e-4c3f-9210-92c9c503767c", "Basic user role", "Basic", null },
                    { new Guid("746537a6-88da-4784-8c5e-690d99ebad1a"), "c19ebd04-b457-4c8d-8e72-75d0845e3b97", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("7852369e-e5ae-4e96-ab56-053669030d9e"), "39883c7c-cb34-458a-a924-f4e84b9d0e6e", "Admin user role", "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("088b414b-944e-484a-81f5-6a675c4c714b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("746537a6-88da-4784-8c5e-690d99ebad1a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7852369e-e5ae-4e96-ab56-053669030d9e"));

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
