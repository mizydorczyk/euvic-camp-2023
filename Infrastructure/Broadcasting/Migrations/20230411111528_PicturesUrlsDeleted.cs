using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Broadcasting.Migrations
{
    /// <inheritdoc />
    public partial class PicturesUrlsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "RadioChannels");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Artists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "RadioChannels",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Pieces",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Artists",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
