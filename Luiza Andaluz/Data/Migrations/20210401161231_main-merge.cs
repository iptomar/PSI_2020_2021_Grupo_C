using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luiza_Andaluz.Data.Migrations
{
    public partial class mainmerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historias_Utilizador_UtilizadorFK",
                table: "Historias");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropIndex(
                name: "IX_Historias_UtilizadorFK",
                table: "Historias");

            migrationBuilder.DropColumn(
                name: "UtilizadorFK",
                table: "Historias");

            migrationBuilder.AddColumn<string>(
                name: "Validador",
                table: "Historias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Validador",
                table: "Historias");

            migrationBuilder.AddColumn<string>(
                name: "UtilizadorFK",
                table: "Historias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historias_UtilizadorFK",
                table: "Historias",
                column: "UtilizadorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Historias_Utilizador_UtilizadorFK",
                table: "Historias",
                column: "UtilizadorFK",
                principalTable: "Utilizador",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
