using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KDramaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTabelaPlaylists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    SpotifyPlaylistId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImagemUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Dono = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TotalMusicas = table.Column<int>(type: "integer", nullable: false),
                    DoramaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Doramas_DoramaId",
                        column: x => x.DoramaId,
                        principalTable: "Doramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_DoramaId",
                table: "Playlists",
                column: "DoramaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
