using Microsoft.EntityFrameworkCore.Migrations;

namespace Luiza_Andaluz.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Ficheiros_HistoriaFK",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Local_LocalFK",
                table: "Ficheiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Utilizador_UtilizadorFK",
                table: "Ficheiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ficheiros",
                table: "Ficheiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Ficheiros",
                newName: "Historias");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Conteudo");

            migrationBuilder.RenameIndex(
                name: "IX_Ficheiros_UtilizadorFK",
                table: "Historias",
                newName: "IX_Historias_UtilizadorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Ficheiros_LocalFK",
                table: "Historias",
                newName: "IX_Historias_LocalFK");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_HistoriaFK",
                table: "Conteudo",
                newName: "IX_Conteudo_HistoriaFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historias",
                table: "Historias",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conteudo",
                table: "Conteudo",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Conteudo_Historias_HistoriaFK",
                table: "Conteudo",
                column: "HistoriaFK",
                principalTable: "Historias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historias_Local_LocalFK",
                table: "Historias",
                column: "LocalFK",
                principalTable: "Local",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historias_Utilizador_UtilizadorFK",
                table: "Historias",
                column: "UtilizadorFK",
                principalTable: "Utilizador",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conteudo_Historias_HistoriaFK",
                table: "Conteudo");

            migrationBuilder.DropForeignKey(
                name: "FK_Historias_Local_LocalFK",
                table: "Historias");

            migrationBuilder.DropForeignKey(
                name: "FK_Historias_Utilizador_UtilizadorFK",
                table: "Historias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historias",
                table: "Historias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conteudo",
                table: "Conteudo");

            migrationBuilder.RenameTable(
                name: "Historias",
                newName: "Ficheiros");

            migrationBuilder.RenameTable(
                name: "Conteudo",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_Historias_UtilizadorFK",
                table: "Ficheiros",
                newName: "IX_Ficheiros_UtilizadorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Historias_LocalFK",
                table: "Ficheiros",
                newName: "IX_Ficheiros_LocalFK");

            migrationBuilder.RenameIndex(
                name: "IX_Conteudo_HistoriaFK",
                table: "Areas",
                newName: "IX_Areas_HistoriaFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ficheiros",
                table: "Ficheiros",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Ficheiros_HistoriaFK",
                table: "Areas",
                column: "HistoriaFK",
                principalTable: "Ficheiros",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Local_LocalFK",
                table: "Ficheiros",
                column: "LocalFK",
                principalTable: "Local",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Utilizador_UtilizadorFK",
                table: "Ficheiros",
                column: "UtilizadorFK",
                principalTable: "Utilizador",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
