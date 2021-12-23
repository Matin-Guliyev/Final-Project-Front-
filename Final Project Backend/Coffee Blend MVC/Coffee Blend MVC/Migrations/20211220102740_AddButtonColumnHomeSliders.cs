using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee_Blend_MVC.Migrations
{
    public partial class AddButtonColumnHomeSliders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Button1",
                table: "HomeSliders",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Button2",
                table: "HomeSliders",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Button1",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "Button2",
                table: "HomeSliders");
        }
    }
}
