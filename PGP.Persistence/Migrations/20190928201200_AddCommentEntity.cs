using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PGP.Persistence.Migrations
{
    public partial class AddCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Pet",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "PetId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CreatedByUserId = table.Column<int>(nullable: false),
                    PetId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => new { x.CreatedByUserId, x.PetId });
                    table.ForeignKey(
                        name: "FK_Comment_CreatedByUser",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Pet",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PetId",
                table: "Comments",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Pet",
                table: "Likes",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Pet",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "PetId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Pet",
                table: "Like",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
