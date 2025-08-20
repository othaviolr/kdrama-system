using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KDramaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NomeCompleto = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    AnoNascimento = table.Column<int>(type: "integer", nullable: true),
                    Altura = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Biografia = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FotoUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doramas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    TituloOriginal = table.Column<string>(type: "text", nullable: true),
                    PaisOrigem = table.Column<string>(type: "text", nullable: false),
                    AnoLancamento = table.Column<int>(type: "integer", nullable: false),
                    EmExibicao = table.Column<bool>(type: "boolean", nullable: false),
                    Sinopse = table.Column<string>(type: "text", nullable: true),
                    ImagemCapaUrl = table.Column<string>(type: "text", nullable: false),
                    Plataforma = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NomeUsuario = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosAutenticacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosAutenticacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoramaAtores",
                columns: table => new
                {
                    DoramaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AtorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoramaAtores", x => new { x.DoramaId, x.AtorId });
                    table.ForeignKey(
                        name: "FK_DoramaAtores_Atores_AtorId",
                        column: x => x.AtorId,
                        principalTable: "Atores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoramaAtores_Doramas_DoramaId",
                        column: x => x.DoramaId,
                        principalTable: "Doramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DoramaId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generos_Doramas_DoramaId",
                        column: x => x.DoramaId,
                        principalTable: "Doramas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Temporadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoramaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AnoLancamento = table.Column<int>(type: "integer", nullable: false),
                    EmExibicao = table.Column<bool>(type: "boolean", nullable: false),
                    Sinopse = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temporadas_Doramas_DoramaId",
                        column: x => x.DoramaId,
                        principalTable: "Doramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoAtividade = table.Column<int>(type: "integer", nullable: false),
                    ReferenciaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atividades_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemporadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nota = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    RecomendadoPorUsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecomendadoPorNomeLivre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DataAvaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    AvaliacaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    TemporadaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Texto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasPrateleiras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ImagemCapaUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Privacidade = table.Column<int>(type: "integer", nullable: false),
                    ShareToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasPrateleiras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasPrateleiras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRelacionamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeguidorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeguindoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRelacionamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioRelacionamentos_Usuarios_SeguidorId",
                        column: x => x.SeguidorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRelacionamentos_Usuarios_SeguindoId",
                        column: x => x.SeguindoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episodios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemporadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Sinopse = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DuracaoMinutos = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    TemporadaId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodios_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodios_Temporadas_TemporadaId1",
                        column: x => x.TemporadaId1,
                        principalTable: "Temporadas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgressoTemporadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemporadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EpisodiosAssistidos = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressoTemporadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressoTemporadas_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressoTemporadas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoramasListas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ListaPrateleiraId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoramaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAdicao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoramasListas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoramasListas_ListasPrateleiras_ListaPrateleiraId",
                        column: x => x.ListaPrateleiraId,
                        principalTable: "ListasPrateleiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_TipoAtividade",
                table: "Atividades",
                column: "TipoAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_UsuarioId",
                table: "Atividades",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_UsuarioId",
                table: "Avaliacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DoramaAtores_AtorId",
                table: "DoramaAtores",
                column: "AtorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoramasListas_ListaPrateleiraId",
                table: "DoramasListas",
                column: "ListaPrateleiraId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_TemporadaId",
                table: "Episodios",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_TemporadaId1",
                table: "Episodios",
                column: "TemporadaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_DoramaId",
                table: "Generos",
                column: "DoramaId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPrateleiras_UsuarioId",
                table: "ListasPrateleiras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressoTemporadas_TemporadaId",
                table: "ProgressoTemporadas",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressoTemporadas_UsuarioId_TemporadaId",
                table: "ProgressoTemporadas",
                columns: new[] { "UsuarioId", "TemporadaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Temporadas_DoramaId",
                table: "Temporadas",
                column: "DoramaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRelacionamentos_SeguidorId_SeguindoId",
                table: "UsuarioRelacionamentos",
                columns: new[] { "SeguidorId", "SeguindoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRelacionamentos_SeguindoId",
                table: "UsuarioRelacionamentos",
                column: "SeguindoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "DoramaAtores");

            migrationBuilder.DropTable(
                name: "DoramasListas");

            migrationBuilder.DropTable(
                name: "Episodios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "ProgressoTemporadas");

            migrationBuilder.DropTable(
                name: "UsuarioRelacionamentos");

            migrationBuilder.DropTable(
                name: "UsuariosAutenticacao");

            migrationBuilder.DropTable(
                name: "Atores");

            migrationBuilder.DropTable(
                name: "ListasPrateleiras");

            migrationBuilder.DropTable(
                name: "Temporadas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Doramas");
        }
    }
}
