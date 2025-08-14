using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KDramaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarListaPrateleiraEDoramaLista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoramaLista_ListasPrateleira_ListaPrateleiraId",
                table: "DoramaLista");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasPrateleira_Usuarios_UsuarioId",
                table: "ListasPrateleira");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListasPrateleira",
                table: "ListasPrateleira");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoramaLista",
                table: "DoramaLista");

            migrationBuilder.RenameTable(
                name: "ListasPrateleira",
                newName: "ListasPrateleiras");

            migrationBuilder.RenameTable(
                name: "DoramaLista",
                newName: "DoramasListas");

            migrationBuilder.RenameIndex(
                name: "IX_ListasPrateleira_UsuarioId",
                table: "ListasPrateleiras",
                newName: "IX_ListasPrateleiras_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_DoramaLista_ListaPrateleiraId",
                table: "DoramasListas",
                newName: "IX_DoramasListas_ListaPrateleiraId");

            migrationBuilder.AddColumn<string>(
                name: "ImagemCapaUrl",
                table: "ListasPrateleiras",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Privacidade",
                table: "ListasPrateleiras",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShareToken",
                table: "ListasPrateleiras",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListasPrateleiras",
                table: "ListasPrateleiras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoramasListas",
                table: "DoramasListas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoramasListas_ListasPrateleiras_ListaPrateleiraId",
                table: "DoramasListas",
                column: "ListaPrateleiraId",
                principalTable: "ListasPrateleiras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasPrateleiras_Usuarios_UsuarioId",
                table: "ListasPrateleiras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoramasListas_ListasPrateleiras_ListaPrateleiraId",
                table: "DoramasListas");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasPrateleiras_Usuarios_UsuarioId",
                table: "ListasPrateleiras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListasPrateleiras",
                table: "ListasPrateleiras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoramasListas",
                table: "DoramasListas");

            migrationBuilder.DropColumn(
                name: "ImagemCapaUrl",
                table: "ListasPrateleiras");

            migrationBuilder.DropColumn(
                name: "Privacidade",
                table: "ListasPrateleiras");

            migrationBuilder.DropColumn(
                name: "ShareToken",
                table: "ListasPrateleiras");

            migrationBuilder.RenameTable(
                name: "ListasPrateleiras",
                newName: "ListasPrateleira");

            migrationBuilder.RenameTable(
                name: "DoramasListas",
                newName: "DoramaLista");

            migrationBuilder.RenameIndex(
                name: "IX_ListasPrateleiras_UsuarioId",
                table: "ListasPrateleira",
                newName: "IX_ListasPrateleira_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_DoramasListas_ListaPrateleiraId",
                table: "DoramaLista",
                newName: "IX_DoramaLista_ListaPrateleiraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListasPrateleira",
                table: "ListasPrateleira",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoramaLista",
                table: "DoramaLista",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoramaLista_ListasPrateleira_ListaPrateleiraId",
                table: "DoramaLista",
                column: "ListaPrateleiraId",
                principalTable: "ListasPrateleira",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasPrateleira_Usuarios_UsuarioId",
                table: "ListasPrateleira",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
