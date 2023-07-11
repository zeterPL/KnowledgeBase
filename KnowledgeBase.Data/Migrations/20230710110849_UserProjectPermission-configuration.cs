using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProjectPermissionconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e5260c4-d258-401d-b5e8-9d876d84c88e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f98b34e-865c-42fe-8947-1316c1d94d62"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b28e1435-ea3e-467a-8a02-a1e60a2a25de"));

            migrationBuilder.AlterColumn<string>(
                name: "PermissionName",
                table: "UserProjectPermission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "PermissionName",
                table: "UserProjectPermission",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3e5260c4-d258-401d-b5e8-9d876d84c88e"), "0fbda9a8-6537-4fc2-adef-6168e9c5a749", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("7f98b34e-865c-42fe-8947-1316c1d94d62"), "00c274da-002b-4642-9be6-1d7f91d148ea", "Admin user role", "Admin", null },
                    { new Guid("b28e1435-ea3e-467a-8a02-a1e60a2a25de"), "416ba37a-ad33-4e00-a833-897024026f7d", "Basic user role", "Basic", null }
                });
        }
    }
}