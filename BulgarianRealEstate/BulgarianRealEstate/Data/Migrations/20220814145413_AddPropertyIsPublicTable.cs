using Microsoft.EntityFrameworkCore.Migrations;

namespace BulgarianRealEstate.Data.Migrations
{
    public partial class AddPropertyIsPublicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Properties");
        }
    }
}
