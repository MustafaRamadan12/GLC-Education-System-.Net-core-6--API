using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GLC.EF.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Students_StudentId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_StudentId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "questionBanks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Quizes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "questionBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_StudentId",
                table: "Quizes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Students_StudentId",
                table: "Quizes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
