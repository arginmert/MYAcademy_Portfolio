using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYAcademy_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaOrder",
                table: "SocialMedias");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SocialMedias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SocialMedias");

            migrationBuilder.AddColumn<int>(
                name: "MediaOrder",
                table: "SocialMedias",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
