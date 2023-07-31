using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class resourcetypediscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Resource",
                newName: "ResourceType");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2e8bf69c-df35-4aca-931b-724d09b13054"), "Admin user role", "Admin" },
                    { new Guid("35274396-9223-4da5-8412-69f55ebe6ee0"), "Basic user role", "Basic" },
                    { new Guid("cf6032fb-5509-453f-a308-eaafb2f20155"), "SuperAdmin user role", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("2e8bf69c-df35-4aca-931b-724d09b13054"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("35274396-9223-4da5-8412-69f55ebe6ee0"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("cf6032fb-5509-453f-a308-eaafb2f20155"));

            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "Resource",
                newName: "Discriminator");

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
    }
}
