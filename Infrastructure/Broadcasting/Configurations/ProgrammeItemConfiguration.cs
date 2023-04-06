using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Broadcasting.Configurations;

public class ProgrammeConfiguration : IEntityTypeConfiguration<ProgrammeItem>
{
    public void Configure(EntityTypeBuilder<ProgrammeItem> builder)
    {
        builder.Property(x => x.PlaybackDate).IsRequired().HasColumnType("timestamp with time zone");
        builder.Property(x => x.Views).IsRequired().HasColumnType("bigint");
        builder.Property(x => x.BroadcastingDuration).IsRequired().HasColumnType("interval");
        builder.Property(x => x.RadioChannelId).IsRequired();

        builder.HasOne(x => x.RadioChannel)
            .WithMany(x => x.ProgrammeItems)
            .HasForeignKey(x => x.RadioChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Piece)
            .WithMany(x => x.ProgrammeItems);
    }
}