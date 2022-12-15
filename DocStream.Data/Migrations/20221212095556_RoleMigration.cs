using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocStream.Data.Migrations
{
    public partial class RoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bankers_Applicants_ApplicantId",
                table: "Bankers");

            migrationBuilder.DropIndex(
                name: "IX_Bankers_ApplicantId",
                table: "Bankers");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Bankers");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ApplicantLegalStatuses",
                columns: new[] { "Id", "BusinessLegalStatus" },
                values: new object[,]
                {
                    { 1, "Sole Proprietor" },
                    { 2, "Partnership" },
                    { 3, "Public Limited Liabilty Company" },
                    { 4, "Private Limited Liabliity Company" },
                    { 5, "Cooperative Society" },
                    { 6, "Other" }
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
                table: "Bankers",
                columns: new[] { "Id", "Address", "BankName", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "13 Marina, Lagos Island, Lagos", "Guarantee Trust Bank", "gtb@gtbhq.com", "+2347893027493" },
                    { 2, "78 Marina, Lagos Island, Lagos", "First Bank", "firstbank@firsthq.com", "+2347856027493" }
                });

            migrationBuilder.InsertData(
                table: "Applicants",
                columns: new[] { "Id", "ApplicantLegalStatusId", "ApplicantName", "Email", "MobileNumber", "PhyscialAddress", "PostalAddress", "ProposedBussinessNameId" },
                values: new object[] { 1, 1, "Bonglis Group", "bonglisgroup@bonglishq.com", "+2347049375663", "13 Marina, Lagos Island, Lagos", "13 Marina, Lagos Island, Lagos", 1 });

            migrationBuilder.InsertData(
                table: "Applicants",
                columns: new[] { "Id", "ApplicantLegalStatusId", "ApplicantName", "Email", "MobileNumber", "PhyscialAddress", "PostalAddress", "ProposedBussinessNameId" },
                values: new object[] { 2, 2, "Nandi Enterprise", "nandienterprise@nandihq.com", "+2347064375663", "59 Marina, Lagos Island, Lagos", "59 Marina, Lagos Island, Lagos", 2 });

            migrationBuilder.InsertData(
                table: "ContactPersons",
                columns: new[] { "Id", "ApplicantId", "ContactPersonName", "Email", "MobileNumber", "PhyscialAddress", "PostalAddress" },
                values: new object[,]
                {
                    { 1, 1, "Ayo Oluwa", "ayooluwa@gmail.com", "+2347049375663", "1 Marina, Lagos Island, Lagos", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "Emeka Okoye", "emekaokoye@gmail.com", "+2347047375663", "7 Marina, Lagos Island, Lagos", "7 Marina, Lagos Island, Lagos" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "ApplicantId", "ConvictionStatus", "CountryOfUsualResidency", "Name", "Nationality", "PhysicalAddress" },
                values: new object[,]
                {
                    { 1, 1, "None", "Nigeria", "Ayo Oluwa", "Nigerian", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "None", "Nigeria", "Emeka Okoye", "Nigerian", "54 Marina, Lagos Island, Lagos" }
                });

            migrationBuilder.InsertData(
                table: "ProposedBussinessNames",
                columns: new[] { "Id", "ApplicantId", "Email", "Location", "Name", "PhoneNumber", "PostalAddress" },
                values: new object[,]
                {
                    { 1, 1, "highplastics@highplastic.com", "Lagos", "High Plastics", "+2347893027983", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "tasteybakery@tasteybakery.com", "Lagos", "Tastey Bakery", "+2348093027983", "100 Marina, Lagos Island, Lagos" }
                });

            migrationBuilder.InsertData(
                table: "Shareholders",
                columns: new[] { "Id", "ApplicantId", "ConvictionStatus", "CountryOfUsualResidency", "Name", "Nationality", "PhysicalAddress" },
                values: new object[,]
                {
                    { 1, 1, "None", "Nigeria", "Ayo Oluwa", "Nigerian", "13 Marina, Lagos Island, Lagos" },
                    { 2, 2, "None", "Nigeria", "Emeka Okoye", "Nigerian", "54 Marina, Lagos Island, Lagos" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eb416e4-a120-4a39-b195-a64f9c13af2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34038e61-fe36-4d05-98ae-cbbdf83b33f7");

            migrationBuilder.DeleteData(
                table: "Bankers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bankers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactPersons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContactPersons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProposedBussinessNames",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProposedBussinessNames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shareholders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shareholders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Applicants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applicants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicantLegalStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Bankers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bankers_ApplicantId",
                table: "Bankers",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bankers_Applicants_ApplicantId",
                table: "Bankers",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
