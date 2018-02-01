using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ControleJogos.Migrations
{
    public partial class EmprestimoAmigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmigoID",
                table: "Emprestimo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_AmigoID",
                table: "Emprestimo",
                column: "AmigoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Amigos_AmigoID",
                table: "Emprestimo",
                column: "AmigoID",
                principalTable: "Amigos",
                principalColumn: "AmigoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Amigos_AmigoID",
                table: "Emprestimo");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimo_AmigoID",
                table: "Emprestimo");

            migrationBuilder.DropColumn(
                name: "AmigoID",
                table: "Emprestimo");
        }
    }
}
