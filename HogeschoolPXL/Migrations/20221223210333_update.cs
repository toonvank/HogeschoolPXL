using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserID",
                table: "Gebruiker");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserID",
                table: "Gebruiker",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserID",
                table: "Gebruiker",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserID",
                table: "Gebruiker");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserID",
                table: "Gebruiker",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_AspNetUsers_IdentityUserID",
                table: "Gebruiker",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
