using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Broadcasting.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnownAs = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PictureUrl = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RadioChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Frequency = table.Column<decimal>(type: "numeric(4,1)", nullable: false),
                    PictureUrl = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    Views = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Version = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ArtistId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pieces_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaybackDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BroadcastingDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    RadioChannelId = table.Column<int>(type: "integer", nullable: false),
                    PieceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgrammeItems_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeItems_RadioChannels_RadioChannelId",
                        column: x => x.RadioChannelId,
                        principalTable: "RadioChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_ArtistId",
                table: "Pieces",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeItems_PieceId",
                table: "ProgrammeItems",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeItems_RadioChannelId",
                table: "ProgrammeItems",
                column: "RadioChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgrammeItems");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "RadioChannels");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
