using Microsoft.EntityFrameworkCore.Migrations;

namespace Luiza_Andaluz.Data.Migrations
{
    public partial class RemocaoDaTabelaAnonimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Utilizadores_AnonimoFK",
                table: "Ficheiros");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropIndex(
                name: "IX_Ficheiros_AnonimoFK",
                table: "Ficheiros");

            migrationBuilder.DropColumn(
                name: "AnonimoFK",
                table: "Ficheiros");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Ficheiros",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Idade",
                table: "Ficheiros",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Ficheiros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Ficheiros");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Ficheiros");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Ficheiros");

            migrationBuilder.AddColumn<string>(
                name: "AnonimoFK",
                table: "Ficheiros",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Idade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_AnonimoFK",
                table: "Ficheiros",
                column: "AnonimoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Utilizadores_AnonimoFK",
                table: "Ficheiros",
                column: "AnonimoFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
