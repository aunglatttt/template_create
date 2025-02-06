using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTest.Migrations
{
    public partial class issdsdds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadingColor",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadingText",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoName",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParagraphColor",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParagraphText",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Width",
                table: "ContentModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadingColor",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "HeadingText",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "LogoName",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "ParagraphColor",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "ParagraphText",
                table: "ContentModels");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ContentModels");
        }
    }
}
