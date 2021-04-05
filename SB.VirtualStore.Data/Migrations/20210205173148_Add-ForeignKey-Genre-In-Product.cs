using Microsoft.EntityFrameworkCore.Migrations;

namespace SB.VirtualStore.Data.Migrations
{
    public partial class AddForeignKeyGenreInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_GenreId",
                table: "Product",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Genre_GenreId",
                table: "Product",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Genre_GenreId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_GenreId",
                table: "Product");
        }
    }
}
