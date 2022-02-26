using Microsoft.EntityFrameworkCore.Migrations;

namespace BulgarianRealEstate.Data.Migrations
{
    public partial class CreateTablePropertyImageUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyImageUrls",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImageUrlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImageUrls", x => new { x.PropertyId, x.ImageUrlId });
                    table.ForeignKey(
                        name: "FK_PropertyImageUrls_ImageUrls_ImageUrlId",
                        column: x => x.ImageUrlId,
                        principalTable: "ImageUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyImageUrls_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageUrls_ImageUrlId",
                table: "PropertyImageUrls",
                column: "ImageUrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImageUrls");
        }
    }
}
