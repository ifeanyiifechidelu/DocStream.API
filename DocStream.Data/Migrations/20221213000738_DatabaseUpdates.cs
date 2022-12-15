using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocStream.Data.Migrations
{
    public partial class DatabaseUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_Applicants_ApplicantId",
                table: "ContactPersons");

            migrationBuilder.DropTable(
                name: "ProposedBussinessNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPersons",
                table: "ContactPersons");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eb416e4-a120-4a39-b195-a64f9c13af2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34038e61-fe36-4d05-98ae-cbbdf83b33f7");

            migrationBuilder.RenameTable(
                name: "ContactPersons",
                newName: "ContactPerple");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPersons_ApplicantId",
                table: "ContactPerple",
                newName: "IX_ContactPerple_ApplicantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPerple",
                table: "ContactPerple",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProposedBusinessNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedBusinessNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposedBusinessNames_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d92e1e3-3094-4916-9e37-7b518389865f", "b8310cac-350f-4a2a-ba12-ad1f2566af5f", "Administrator", "ADMINISTRATOR" },
                    { "fbb2c206-2e8c-4d90-a9df-66af7e56b0f5", "26771ca2-7243-4bbe-81e9-138b9e30b56c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ProposedBusinessNames",
                columns: new[] { "Id", "ApplicantId", "Email", "Location", "Name", "PhoneNumber", "PostalAddress" },
                values: new object[,]
                {
                    { 1, 1, "highplastics@highplastic.com", "Lagos", "High Plastics", "+2347893027983", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "tasteybakery@tasteybakery.com", "Lagos", "Tastey Bakery", "+2348093027983", "100 Marina, Lagos Island, Lagos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProposedBusinessNames_ApplicantId",
                table: "ProposedBusinessNames",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPerple_Applicants_ApplicantId",
                table: "ContactPerple",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPerple_Applicants_ApplicantId",
                table: "ContactPerple");

            migrationBuilder.DropTable(
                name: "ProposedBusinessNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPerple",
                table: "ContactPerple");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d92e1e3-3094-4916-9e37-7b518389865f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbb2c206-2e8c-4d90-a9df-66af7e56b0f5");

            migrationBuilder.RenameTable(
                name: "ContactPerple",
                newName: "ContactPersons");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPerple_ApplicantId",
                table: "ContactPersons",
                newName: "IX_ContactPersons_ApplicantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPersons",
                table: "ContactPersons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProposedBussinessNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedBussinessNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposedBussinessNames_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eb416e4-a120-4a39-b195-a64f9c13af2d", "cb88d06b-2da0-4017-827b-606934557a7f", "Administrator", "ADMINISTRATOR" },
                    { "34038e61-fe36-4d05-98ae-cbbdf83b33f7", "2f2de843-a512-4d2b-b7f8-c42956d83a67", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ProposedBussinessNames",
                columns: new[] { "Id", "ApplicantId", "Email", "Location", "Name", "PhoneNumber", "PostalAddress" },
                values: new object[,]
                {
                    { 1, 1, "highplastics@highplastic.com", "Lagos", "High Plastics", "+2347893027983", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "tasteybakery@tasteybakery.com", "Lagos", "Tastey Bakery", "+2348093027983", "100 Marina, Lagos Island, Lagos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProposedBussinessNames_ApplicantId",
                table: "ProposedBussinessNames",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_Applicants_ApplicantId",
                table: "ContactPersons",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
