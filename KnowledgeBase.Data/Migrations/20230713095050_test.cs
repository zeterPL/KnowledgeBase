using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
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

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("036fb46a-4dbc-48c5-991d-5f0a4de7edc7"), "Admin user role", "Admin" },
                    { new Guid("3868f7d2-e8af-441a-b57b-07f71f7aa56d"), "Basic user role", "Basic" },
                    { new Guid("9abbedf1-79bf-4238-9287-927f8466a063"), "SuperAdmin user role", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("036fb46a-4dbc-48c5-991d-5f0a4de7edc7"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("3868f7d2-e8af-441a-b57b-07f71f7aa56d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("9abbedf1-79bf-4238-9287-927f8466a063"));

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
