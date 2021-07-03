using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SB.VirtualStore.Data.Migrations
{
    public partial class UpdateConfigurationLogoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "ConfigurationSite",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(1)",
                oldMaxLength: 1,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "ConfigurationSite",
                type: "varbinary(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);
        }
    }
}
