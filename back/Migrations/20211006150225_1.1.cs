using Microsoft.EntityFrameworkCore.Migrations;

namespace back.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealeaseDate",
                table: "Playlists",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "RealeaseDate",
                table: "Musics",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Playlists",
                newName: "RealeaseDate");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Musics",
                newName: "RealeaseDate");
        }
    }
}
