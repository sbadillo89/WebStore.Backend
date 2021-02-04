using Microsoft.EntityFrameworkCore.Migrations;

namespace SB.VirtualStore.Data.Migrations
{
    public partial class SeactualizamodelconfigurationSitconesconcampoActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ConfigurationSite",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ConfigurationSite");
        }
    }
}
