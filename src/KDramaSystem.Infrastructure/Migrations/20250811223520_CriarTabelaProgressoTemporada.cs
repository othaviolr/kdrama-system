using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KDramaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaProgressoTemporada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgressoTemporadas_UsuarioId",
                table: "ProgressoTemporadas");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressoTemporadas_UsuarioId_TemporadaId",
                table: "ProgressoTemporadas",
                columns: new[] { "UsuarioId", "TemporadaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgressoTemporadas_UsuarioId_TemporadaId",
                table: "ProgressoTemporadas");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressoTemporadas_UsuarioId",
                table: "ProgressoTemporadas",
                column: "UsuarioId");
        }
    }
}
