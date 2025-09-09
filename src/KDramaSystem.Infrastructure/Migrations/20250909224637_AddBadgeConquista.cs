using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KDramaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBadgeConquista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Raridade = table.Column<int>(type: "integer", nullable: false),
                    Pontos = table.Column<int>(type: "integer", nullable: false),
                    Categoria = table.Column<int>(type: "integer", nullable: false),
                    Condicao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IconeUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadgesConquistadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    BadgeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataConquista = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgesConquistadas", x => x.Id);
                });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Avaliacoes_TemporadaId",
            //     table: "Avaliacoes",
            //     column: "TemporadaId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Avaliacoes_Temporadas_TemporadaId",
            //     table: "Avaliacoes",
            //     column: "TemporadaId",
            //     principalTable: "Temporadas",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Temporadas_TemporadaId",
                table: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "BadgesConquistadas");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_TemporadaId",
                table: "Avaliacoes");
        }
    }
}
