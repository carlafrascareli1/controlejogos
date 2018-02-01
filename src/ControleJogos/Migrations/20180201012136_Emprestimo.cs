using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ControleJogos.Migrations
{
    public partial class Emprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emprestimo",
                columns: table => new
                {
                    EmprestimoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataDevolucao = table.Column<DateTime>(nullable: true),
                    DataDevolucaoPrevista = table.Column<DateTime>(nullable: false),
                    DataEmprestimo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimo", x => x.EmprestimoID);
                });

            migrationBuilder.CreateTable(
                name: "EmprestimoJogo",
                columns: table => new
                {
                    JogoID = table.Column<int>(nullable: false),
                    EmprestimoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprestimoJogo", x => new { x.JogoID, x.EmprestimoID });
                    table.ForeignKey(
                        name: "FK_EmprestimoJogo_Emprestimo_EmprestimoID",
                        column: x => x.EmprestimoID,
                        principalTable: "Emprestimo",
                        principalColumn: "EmprestimoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmprestimoJogo_Jogos_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogos",
                        principalColumn: "JogoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmprestimoJogo_EmprestimoID",
                table: "EmprestimoJogo",
                column: "EmprestimoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmprestimoJogo");

            migrationBuilder.DropTable(
                name: "Emprestimo");
        }
    }
}
