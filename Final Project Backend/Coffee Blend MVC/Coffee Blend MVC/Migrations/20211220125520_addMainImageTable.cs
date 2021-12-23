using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee_Blend_MVC.Migrations
{
    public partial class addMainImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Head = table.Column<string>(maxLength: 255, nullable: true),
                    Text = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainImages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainImages");
        }
    }
}
