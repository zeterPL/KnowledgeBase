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
                keyValue: new Guid("76690512-155d-4a50-a376-c682aa4cd066"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("8559a4db-a374-4545-960b-600ae779133e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("bb1e8e41-26b2-4554-ad70-4e5a98d63254"));

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
                name: "Target",
                table: "Resource",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("7b67874b-5b30-45f4-90b6-833ff410e532"), "Admin user role", "Admin" },
                    { new Guid("c096fb31-7951-4f46-a652-f83bbf19230c"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("df491d6f-29f0-4471-924a-ee71978ee09c"), "Basic user role", "Basic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("7b67874b-5b30-45f4-90b6-833ff410e532"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c096fb31-7951-4f46-a652-f83bbf19230c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("df491d6f-29f0-4471-924a-ee71978ee09c"));

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
                name: "Target",
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
