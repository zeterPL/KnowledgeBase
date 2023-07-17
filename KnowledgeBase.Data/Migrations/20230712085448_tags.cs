using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnowledgeBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e4aad75-a51e-4fb7-8061-f4b98f7c61fc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("565a5e47-d75c-4e8d-a8a1-b75e21a60c0d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("868773a4-1051-44ae-9744-97bae50614c8"));

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => new { x.ProjectId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProjectTag_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("65fa9115-2873-461b-a196-44db3926f65e"), "c5558356-0464-404c-90e2-c54f592fb103", "Admin user role", "Admin", null },
                    { new Guid("973555d1-af41-4e54-8420-cb9440b681d7"), "3c480873-76d2-4feb-b49e-d2e5a5f85e25", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("9b2471bc-e525-4d77-967e-f536104be37d"), "92339bb2-9429-4a03-b964-afca475b92b8", "Basic user role", "Basic", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_TagId",
                table: "ProjectTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("65fa9115-2873-461b-a196-44db3926f65e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("973555d1-af41-4e54-8420-cb9440b681d7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9b2471bc-e525-4d77-967e-f536104be37d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2e4aad75-a51e-4fb7-8061-f4b98f7c61fc"), "be689ce3-4fd7-4531-888f-6458ae8a8662", "Admin user role", "Admin", null },
                    { new Guid("565a5e47-d75c-4e8d-a8a1-b75e21a60c0d"), "373000eb-1cfa-48a6-85df-32ae0939956b", "SuperAdmin user role", "SuperAdmin", null },
                    { new Guid("868773a4-1051-44ae-9744-97bae50614c8"), "273eb104-9c38-4ce4-96bc-08c40b0bbe6f", "Basic user role", "Basic", null }
                });
        }
    }
}
