using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class vaklectoren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lectoren_GebruikerID",
                table: "Lectoren",
                column: "GebruikerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectoren_Gebruiker_GebruikerID",
                table: "Lectoren",
                column: "GebruikerID",
                principalTable: "Gebruiker",
                principalColumn: "GebruikerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectoren_Gebruiker_GebruikerID",
                table: "Lectoren");

            migrationBuilder.DropIndex(
                name: "IX_Lectoren_GebruikerID",
                table: "Lectoren");
        }
    }
}
