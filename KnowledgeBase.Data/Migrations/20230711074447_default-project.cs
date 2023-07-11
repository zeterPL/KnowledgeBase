using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class defaultproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("95aded9c-9e4f-494d-91bd-345da07b8bb0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9af19515-fc02-4e1a-9621-8e1efbd887cb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d3ca54d2-dcee-4587-b61d-c369cea08f47"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2e33ab6c-efea-4f62-bdf2-e55e741389c9"), "4259e199-be62-459a-8d5c-6415ebc74fb1", "Admin user role", "Admin", null },
                    { new Guid("7a625259-e4e7-4409-9930-7bb58ee22eaf"), "c2d27718-4921-4a5e-8599-0eebf393753c", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("d447125f-b29a-4cd6-b1db-969223021451"), "9b2036b4-7d0c-4506-be54-0c5057b4c446", "Basic user role", "Basic", null }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"), false, "Deafult project" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e33ab6c-efea-4f62-bdf2-e55e741389c9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a625259-e4e7-4409-9930-7bb58ee22eaf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d447125f-b29a-4cd6-b1db-969223021451"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("95aded9c-9e4f-494d-91bd-345da07b8bb0"), "5f1cf79a-381c-4a0c-b4eb-21bfc9bac71d", "Admin user role", "Admin", null },
                    { new Guid("9af19515-fc02-4e1a-9621-8e1efbd887cb"), "1a881a3c-037c-4f61-af40-21cf6eae438e", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("d3ca54d2-dcee-4587-b61d-c369cea08f47"), "0f2ed583-4a24-4ccd-8ae3-7a32c0cbb106", "Basic user role", "Basic", null }
                });
        }
    }
}
