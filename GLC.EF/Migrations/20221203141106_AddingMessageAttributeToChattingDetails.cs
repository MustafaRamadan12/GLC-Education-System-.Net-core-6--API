using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GLC.EF.Migrations
{
    public partial class AddingMessageAttributeToChattingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ChatingDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ChatingDetails");
        }
    }
}
