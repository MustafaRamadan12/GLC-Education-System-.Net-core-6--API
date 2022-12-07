using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GLC.EF.Migrations
{
    public partial class AddingDateTimeAttributeToChatingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "ChatingDetails",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "ChatingDetails");
        }
    }
}
