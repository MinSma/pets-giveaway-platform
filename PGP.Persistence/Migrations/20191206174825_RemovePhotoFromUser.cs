using Microsoft.EntityFrameworkCore.Migrations;

namespace PGP.Persistence.Migrations
{
    public partial class RemovePhotoFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Pets",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Pets",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
