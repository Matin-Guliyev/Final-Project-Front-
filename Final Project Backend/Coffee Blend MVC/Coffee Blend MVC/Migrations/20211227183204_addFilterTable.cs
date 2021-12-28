using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee_Blend_MVC.Migrations
{
    public partial class addFilterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDessert",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDrink",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainDish",
                table: "Filters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDessert",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "IsDrink",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "IsMainDish",
                table: "Filters");
        }
    }
}
