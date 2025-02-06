using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTest.Migrations
{
    public partial class issdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_Templates_TemplateId",
                table: "DynamicFields");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "DynamicFields",
                newName: "TemplateNotusedId");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicFields_TemplateId",
                table: "DynamicFields",
                newName: "IX_DynamicFields_TemplateNotusedId");

            migrationBuilder.CreateTable(
                name: "TemplateModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentModels_TemplateModels_TemplateModelId",
                        column: x => x.TemplateModelId,
                        principalTable: "TemplateModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentModels_TemplateModelId",
                table: "ContentModels",
                column: "TemplateModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_Templates_TemplateNotusedId",
                table: "DynamicFields",
                column: "TemplateNotusedId",
                principalTable: "Templates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_Templates_TemplateNotusedId",
                table: "DynamicFields");

            migrationBuilder.DropTable(
                name: "ContentModels");

            migrationBuilder.DropTable(
                name: "TemplateModels");

            migrationBuilder.RenameColumn(
                name: "TemplateNotusedId",
                table: "DynamicFields",
                newName: "TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicFields_TemplateNotusedId",
                table: "DynamicFields",
                newName: "IX_DynamicFields_TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_Templates_TemplateId",
                table: "DynamicFields",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }
    }
}
