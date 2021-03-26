using Microsoft.EntityFrameworkCore.Migrations;

namespace Luiza_Andaluz.Data.Migrations
{
    public partial class NomesErrados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Comentarios_LocalFK",
                table: "Ficheiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Downloads_UtilizadorFK",
                table: "Ficheiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Downloads",
                table: "Downloads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.RenameTable(
                name: "Downloads",
                newName: "Utilizador");

            migrationBuilder.RenameTable(
                name: "Comentarios",
                newName: "Local");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizador",
                table: "Utilizador",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Local",
                table: "Local",
                column: "ID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Local_LocalFK",
                table: "Ficheiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Utilizador_UtilizadorFK",
                table: "Ficheiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizador",
                table: "Utilizador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Local",
                table: "Local");

            migrationBuilder.RenameTable(
                name: "Utilizador",
                newName: "Downloads");

            migrationBuilder.RenameTable(
                name: "Local",
                newName: "Comentarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Downloads",
                table: "Downloads",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Comentarios_LocalFK",
                table: "Ficheiros",
                column: "LocalFK",
                principalTable: "Comentarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Downloads_UtilizadorFK",
                table: "Ficheiros",
                column: "UtilizadorFK",
                principalTable: "Downloads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
