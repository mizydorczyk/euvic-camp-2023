using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Broadcasting.Configurations;

public class PieceConfiguration : IEntityTypeConfiguration<Piece>
{
    public void Configure(EntityTypeBuilder<Piece> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(32);
        builder.Property(x => x.ReleaseDate).IsRequired().HasColumnType("date");
        builder.Property(x => x.Duration).IsRequired().HasColumnType("interval");
        builder.Property(x => x.Version).HasMaxLength(32);
        builder.Property(x => x.ArtistId).IsRequired();

        builder.HasOne(x => x.Artist)
            .WithMany(x => x.Pieces)
            .HasForeignKey(x => x.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}