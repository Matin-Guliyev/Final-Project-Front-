using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee_Blend_MVC.Migrations
{
    public partial class CofeeBlend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeSliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(maxLength: 255, nullable: false),
                    Text1 = table.Column<string>(maxLength: 255, nullable: false),
                    Text2 = table.Column<string>(maxLength: 255, nullable: true),
                    Text3 = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSliders");
        }
    }
}
