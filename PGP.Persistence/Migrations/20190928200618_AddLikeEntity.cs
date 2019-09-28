using Microsoft.EntityFrameworkCore.Migrations;

namespace PGP.Persistence.Migrations
{
    public partial class AddLikeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Types_CategoryId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Pets_PetId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_PetId",
                table: "Photos",
                newName: "IX_Photos_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    PetId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.PetId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Like_Pet",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Categories_CategoryId",
                table: "Pets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Pets_PetId",
                table: "Photos",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Categories_CategoryId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Pets_PetId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Types");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PetId",
                table: "Photo",
                newName: "IX_Photo_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Types_CategoryId",
                table: "Pets",
                column: "CategoryId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Pets_PetId",
                table: "Photo",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
