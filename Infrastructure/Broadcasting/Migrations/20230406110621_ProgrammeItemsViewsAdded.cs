using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Broadcasting.Migrations
{
    /// <inheritdoc />
    public partial class ProgrammeItemsViewsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Pieces");

            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "ProgrammeItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "ProgrammeItems");

            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "Pieces",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
