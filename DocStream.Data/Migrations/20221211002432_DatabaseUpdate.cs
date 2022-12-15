using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocStream.Data.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Shareholders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "ProposedBussinessNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Directors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "ContactPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Bankers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "ApplicantLegalStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_ApplicantId",
                table: "Shareholders",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposedBussinessNames_ApplicantId",
                table: "ProposedBussinessNames",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_ApplicantId",
                table: "Directors",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_ApplicantId",
                table: "ContactPersons",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bankers_ApplicantId",
                table: "Bankers",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantLegalStatuses_ApplicantId",
                table: "ApplicantLegalStatuses",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantLegalStatuses_Applicants_ApplicantId",
                table: "ApplicantLegalStatuses",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bankers_Applicants_ApplicantId",
                table: "Bankers",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_Applicants_ApplicantId",
                table: "ContactPersons",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Applicants_ApplicantId",
                table: "Directors",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedBussinessNames_Applicants_ApplicantId",
                table: "ProposedBussinessNames",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shareholders_Applicants_ApplicantId",
                table: "Shareholders",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantLegalStatuses_Applicants_ApplicantId",
                table: "ApplicantLegalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Bankers_Applicants_ApplicantId",
                table: "Bankers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_Applicants_ApplicantId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Applicants_ApplicantId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposedBussinessNames_Applicants_ApplicantId",
                table: "ProposedBussinessNames");

            migrationBuilder.DropForeignKey(
                name: "FK_Shareholders_Applicants_ApplicantId",
                table: "Shareholders");

            migrationBuilder.DropIndex(
                name: "IX_Shareholders_ApplicantId",
                table: "Shareholders");

            migrationBuilder.DropIndex(
                name: "IX_ProposedBussinessNames_ApplicantId",
                table: "ProposedBussinessNames");

            migrationBuilder.DropIndex(
                name: "IX_Directors_ApplicantId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_ApplicantId",
                table: "ContactPersons");

            migrationBuilder.DropIndex(
                name: "IX_Bankers_ApplicantId",
                table: "Bankers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantLegalStatuses_ApplicantId",
                table: "ApplicantLegalStatuses");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Shareholders");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "ProposedBussinessNames");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Bankers");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "ApplicantLegalStatuses");
        }
    }
}
