using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class projectnewproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("2f205e91-e7dc-48f2-b33c-d0124d6c86f5"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("8cc6c944-b7b2-45e6-8b24-bab95006e5fd"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("efc09b8c-d2a0-41eb-b9df-d10361ce9165"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Project",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("809d55dc-266c-4171-adc5-81dd92d4d23a"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("f09c9f85-217a-450d-be6a-21cdbdae4621"), "Admin user role", "Admin" },
                    { new Guid("fd728481-ecbd-45f4-9ad2-03698c1bcb4c"), "Basic user role", "Basic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Project");

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"), false, "Deafult project" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2f205e91-e7dc-48f2-b33c-d0124d6c86f5"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("8cc6c944-b7b2-45e6-8b24-bab95006e5fd"), "Admin user role", "Admin" },
                    { new Guid("efc09b8c-d2a0-41eb-b9df-d10361ce9165"), "Basic user role", "Basic" }
                });
        }
    }
}
