using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocStream.Data.Migrations
{
    public partial class FileUploadMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d92e1e3-3094-4916-9e37-7b518389865f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbb2c206-2e8c-4d90-a9df-66af7e56b0f5");

            migrationBuilder.CreateTable(
                name: "FileDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FileDetails_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51856da9-09d8-4068-a8c6-cbedd0d5649f", "f92e5c19-3d03-479e-ae57-b7b6eefa4aa5", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cebc6b5-afc4-4f43-9077-c145844a01a8", "29db26b1-9052-4a1a-8808-a0be6c687e0b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_FileDetails_ApplicantId",
                table: "FileDetails",
                column: "ApplicantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51856da9-09d8-4068-a8c6-cbedd0d5649f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cebc6b5-afc4-4f43-9077-c145844a01a8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d92e1e3-3094-4916-9e37-7b518389865f", "b8310cac-350f-4a2a-ba12-ad1f2566af5f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbb2c206-2e8c-4d90-a9df-66af7e56b0f5", "26771ca2-7243-4bbe-81e9-138b9e30b56c", "User", "USER" });
        }
    }
}
