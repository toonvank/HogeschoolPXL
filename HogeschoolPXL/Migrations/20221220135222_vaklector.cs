using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class vaklector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Gebruiker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VakLectoren_LectorID",
                table: "VakLectoren",
                column: "LectorID");

            migrationBuilder.CreateIndex(
                name: "IX_VakLectoren_VakId",
                table: "VakLectoren",
                column: "VakId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_IdentityUserId",
                table: "Gebruiker",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserId",
                table: "Gebruiker",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VakLectoren_Lectoren_LectorID",
                table: "VakLectoren",
                column: "LectorID",
                principalTable: "Lectoren",
                principalColumn: "LectorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren",
                column: "VakId",
                principalTable: "Vakken",
                principalColumn: "VakId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserId",
                table: "Gebruiker");

            migrationBuilder.DropForeignKey(
                name: "FK_VakLectoren_Lectoren_LectorID",
                table: "VakLectoren");

            migrationBuilder.DropForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren");

            migrationBuilder.DropIndex(
                name: "IX_VakLectoren_LectorID",
                table: "VakLectoren");

            migrationBuilder.DropIndex(
                name: "IX_VakLectoren_VakId",
                table: "VakLectoren");

            migrationBuilder.DropIndex(
                name: "IX_Gebruiker_IdentityUserId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Gebruiker");
        }
    }
}
