using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class vaklectorid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren");

            migrationBuilder.AlterColumn<int>(
                name: "VakId",
                table: "VakLectoren",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren",
                column: "VakId",
                principalTable: "Vakken",
                principalColumn: "VakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren");

            migrationBuilder.AlterColumn<int>(
                name: "VakId",
                table: "VakLectoren",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VakLectoren_Vakken_VakId",
                table: "VakLectoren",
                column: "VakId",
                principalTable: "Vakken",
                principalColumn: "VakId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
