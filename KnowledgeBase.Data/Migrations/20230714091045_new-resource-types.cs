using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class newresourcetypes : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "AzureStorageAbsolutePath",
                table: "Resource",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)",
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "AzureFileName",
                table: "Resource",
                type: "nvarchar(104)",
                maxLength: 104,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(104)",
                oldMaxLength: 104);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Resource",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Resource",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Resource",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Where",
                table: "Resource",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("01718026-1ed4-4557-865e-16235b81ee1b"), "Admin user role", "Admin" },
                    { new Guid("8a77c6c3-6efe-44c8-a883-2444b1b95991"), "Basic user role", "Basic" },
                    { new Guid("eb947012-e259-4da3-81e3-7b27b14a8197"), "SuperAdmin user role", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("01718026-1ed4-4557-865e-16235b81ee1b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("8a77c6c3-6efe-44c8-a883-2444b1b95991"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eb947012-e259-4da3-81e3-7b27b14a8197"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "Where",
                table: "Resource");

            migrationBuilder.AlterColumn<string>(
                name: "AzureStorageAbsolutePath",
                table: "Resource",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(225)",
                oldMaxLength: 225,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AzureFileName",
                table: "Resource",
                type: "nvarchar(104)",
                maxLength: 104,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(104)",
                oldMaxLength: 104,
                oldNullable: true);

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
