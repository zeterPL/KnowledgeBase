using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class creationDateIntoProjectWithDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("2c65a49a-d32f-403b-85ec-0428253b37d1"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("45ab3d63-9776-4af1-8b6c-613fa83c328d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c648c023-eece-4c71-9f84-1ba51f288d39"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 21, 10, 5, 11, 753, DateTimeKind.Local).AddTicks(976),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 9, 8, 31, 126, DateTimeKind.Local).AddTicks(2270));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"),
                column: "CreationDate",
                value: new DateTime(2023, 7, 21, 10, 5, 11, 753, DateTimeKind.Local).AddTicks(727));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3f97df26-b352-4021-b8da-ebc7cff9b0c9"), "Basic user role", "Basic" },
                    { new Guid("790ff1d6-0d9c-4966-b6f5-efa1aca66250"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("f95efe6a-9f54-4ebe-b17d-f7cbc6977a71"), "Admin user role", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("3f97df26-b352-4021-b8da-ebc7cff9b0c9"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("790ff1d6-0d9c-4966-b6f5-efa1aca66250"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f95efe6a-9f54-4ebe-b17d-f7cbc6977a71"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 9, 8, 31, 126, DateTimeKind.Local).AddTicks(2270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 21, 10, 5, 11, 753, DateTimeKind.Local).AddTicks(976));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"),
                column: "CreationDate",
                value: new DateTime(2023, 7, 20, 9, 8, 31, 126, DateTimeKind.Local).AddTicks(1544));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2c65a49a-d32f-403b-85ec-0428253b37d1"), "Basic user role", "Basic" },
                    { new Guid("45ab3d63-9776-4af1-8b6c-613fa83c328d"), "Admin user role", "Admin" },
                    { new Guid("c648c023-eece-4c71-9f84-1ba51f288d39"), "SuperAdmin user role", "SuperAdmin" }
                });
        }
    }
}
