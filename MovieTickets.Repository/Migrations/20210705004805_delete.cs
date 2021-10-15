using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTickets.Repository.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MovieInOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MovieInOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
