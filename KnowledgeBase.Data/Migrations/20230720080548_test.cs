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
                keyValue: new Guid("5b87b3c7-b5c4-40d8-861c-9ecf361e9033"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("87e5094e-7ab5-4e18-b5c9-b4bcdb05b22d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("acbe6901-c569-49be-999a-5f85ff15a905"));

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
                name: "ResourceType",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "Resource",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserResourcePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResourcePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserResourcePermission_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResourcePermission_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2c65a49a-d32f-403b-85ec-0428253b37d1"), "Basic user role", "Basic" },
                    { new Guid("45ab3d63-9776-4af1-8b6c-613fa83c328d"), "Admin user role", "Admin" },
                    { new Guid("c648c023-eece-4c71-9f84-1ba51f288d39"), "SuperAdmin user role", "SuperAdmin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserResourcePermission_ResourceId",
                table: "UserResourcePermission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResourcePermission_UserId",
                table: "UserResourcePermission",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResourcePermission");

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
                name: "ResourceType",
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
                    { new Guid("5b87b3c7-b5c4-40d8-861c-9ecf361e9033"), "Basic user role", "Basic" },
                    { new Guid("87e5094e-7ab5-4e18-b5c9-b4bcdb05b22d"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("acbe6901-c569-49be-999a-5f85ff15a905"), "Admin user role", "Admin" }
                });
        }
    }
}
