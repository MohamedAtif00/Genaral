using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesSites.Migrations
{
    public partial class addingjuncActorMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_actors_ActorId",
                table: "ActorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_movies_MovieId",
                table: "ActorMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorMovie",
                table: "ActorMovie");

            migrationBuilder.RenameTable(
                name: "ActorMovie",
                newName: "actorMovies");

            migrationBuilder.RenameIndex(
                name: "IX_ActorMovie_MovieId",
                table: "actorMovies",
                newName: "IX_actorMovies_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "MovieViewModelId",
                table: "actorMovies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_actorMovies",
                table: "actorMovies",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.CreateTable(
                name: "MovieViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actorMovies_MovieViewModelId",
                table: "actorMovies",
                column: "MovieViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_actorMovies_actors_ActorId",
                table: "actorMovies",
                column: "ActorId",
                principalTable: "actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_actorMovies_movies_MovieId",
                table: "actorMovies",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_actorMovies_MovieViewModel_MovieViewModelId",
                table: "actorMovies",
                column: "MovieViewModelId",
                principalTable: "MovieViewModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actorMovies_actors_ActorId",
                table: "actorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_actorMovies_movies_MovieId",
                table: "actorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_actorMovies_MovieViewModel_MovieViewModelId",
                table: "actorMovies");

            migrationBuilder.DropTable(
                name: "MovieViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actorMovies",
                table: "actorMovies");

            migrationBuilder.DropIndex(
                name: "IX_actorMovies_MovieViewModelId",
                table: "actorMovies");

            migrationBuilder.DropColumn(
                name: "MovieViewModelId",
                table: "actorMovies");

            migrationBuilder.RenameTable(
                name: "actorMovies",
                newName: "ActorMovie");

            migrationBuilder.RenameIndex(
                name: "IX_actorMovies_MovieId",
                table: "ActorMovie",
                newName: "IX_ActorMovie_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorMovie",
                table: "ActorMovie",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_actors_ActorId",
                table: "ActorMovie",
                column: "ActorId",
                principalTable: "actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_movies_MovieId",
                table: "ActorMovie",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
