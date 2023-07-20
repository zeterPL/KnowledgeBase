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
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("6eefdab9-5ddc-4629-b53f-d497da886e0b"));
========
                keyValue: new Guid("2e8bf69c-df35-4aca-931b-724d09b13054"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("79f89f50-d520-49f1-bbdf-adb13a5f8a96"));
========
                keyValue: new Guid("35274396-9223-4da5-8412-69f55ebe6ee0"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("9abfb461-0a83-4094-8e36-171d84aa9d1e"));
========
                keyValue: new Guid("cf6032fb-5509-453f-a308-eaafb2f20155"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

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
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                    { new Guid("2f205e91-e7dc-48f2-b33c-d0124d6c86f5"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("8cc6c944-b7b2-45e6-8b24-bab95006e5fd"), "Admin user role", "Admin" },
                    { new Guid("efc09b8c-d2a0-41eb-b9df-d10361ce9165"), "Basic user role", "Basic" }
========
                    { new Guid("07e5c474-1395-4b16-840b-58b5a69fe53d"), "Basic user role", "Basic" },
                    { new Guid("3632deb5-0642-4d30-b5d8-0a21028e1d1a"), "SuperAdmin user role", "SuperAdmin" },
                    { new Guid("ddd1b4a9-7db5-463d-af45-a166d97d9ed0"), "Admin user role", "Admin" }
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs
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
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("2f205e91-e7dc-48f2-b33c-d0124d6c86f5"));
========
                keyValue: new Guid("07e5c474-1395-4b16-840b-58b5a69fe53d"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("8cc6c944-b7b2-45e6-8b24-bab95006e5fd"));
========
                keyValue: new Guid("3632deb5-0642-4d30-b5d8-0a21028e1d1a"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                keyValue: new Guid("efc09b8c-d2a0-41eb-b9df-d10361ce9165"));
========
                keyValue: new Guid("ddd1b4a9-7db5-463d-af45-a166d97d9ed0"));
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
<<<<<<<< HEAD:KnowledgeBase.Data/Migrations/20230719072919_tags.cs
                    { new Guid("6eefdab9-5ddc-4629-b53f-d497da886e0b"), "Basic user role", "Basic" },
                    { new Guid("79f89f50-d520-49f1-bbdf-adb13a5f8a96"), "Admin user role", "Admin" },
                    { new Guid("9abfb461-0a83-4094-8e36-171d84aa9d1e"), "SuperAdmin user role", "SuperAdmin" }
========
                    { new Guid("2e8bf69c-df35-4aca-931b-724d09b13054"), "Admin user role", "Admin" },
                    { new Guid("35274396-9223-4da5-8412-69f55ebe6ee0"), "Basic user role", "Basic" },
                    { new Guid("cf6032fb-5509-453f-a308-eaafb2f20155"), "SuperAdmin user role", "SuperAdmin" }
>>>>>>>> test:KnowledgeBase.Data/Migrations/20230718072656_tags.cs
                });
        }
    }
}
