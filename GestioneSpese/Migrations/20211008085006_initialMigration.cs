using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestioneSpese.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Spese",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaID = table.Column<int>(type: "int", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Utente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Approvato = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spese_Categorie_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorie",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spese_CategoriaID",
                table: "Spese",
                column: "CategoriaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spese");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}
