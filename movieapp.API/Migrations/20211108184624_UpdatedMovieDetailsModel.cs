using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movieapp.API.Migrations
{
    public partial class UpdatedMovieDetailsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_MoviesDetails_MovieDetailsId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_MoviesDetails_MovieDetailsId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_MovieDetailsId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieDetailsId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "MoviesDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "MoviesDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "MoviesDetails");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "MoviesDetails");

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieDetailsId",
                table: "Genres",
                column: "MovieDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieDetailsId",
                table: "Actors",
                column: "MovieDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_MoviesDetails_MovieDetailsId",
                table: "Actors",
                column: "MovieDetailsId",
                principalTable: "MoviesDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_MoviesDetails_MovieDetailsId",
                table: "Genres",
                column: "MovieDetailsId",
                principalTable: "MoviesDetails",
                principalColumn: "Id");
        }
    }
}
