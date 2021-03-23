using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luiza_Andaluz.Data.Migrations
{
    public partial class CriacaoBaseDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    Longitude = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    Aut = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Idade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ficheiros",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    LocalFK = table.Column<string>(nullable: false),
                    UtilizadorFK = table.Column<string>(nullable: false),
                    AnonimoFK = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficheiros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Utilizadores_AnonimoFK",
                        column: x => x.AnonimoFK,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Comentarios_LocalFK",
                        column: x => x.LocalFK,
                        principalTable: "Comentarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Downloads_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Downloads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Ficheiro = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    HistoriaFK = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Ficheiros_HistoriaFK",
                        column: x => x.HistoriaFK,
                        principalTable: "Ficheiros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_HistoriaFK",
                table: "Areas",
                column: "HistoriaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_AnonimoFK",
                table: "Ficheiros",
                column: "AnonimoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_LocalFK",
                table: "Ficheiros",
                column: "LocalFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_UtilizadorFK",
                table: "Ficheiros",
                column: "UtilizadorFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Ficheiros");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Downloads");
        }
    }
}
