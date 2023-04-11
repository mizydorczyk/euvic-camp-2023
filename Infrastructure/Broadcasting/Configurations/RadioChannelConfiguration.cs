using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Broadcasting.Configurations;

public class RadioChannelConfiguration : IEntityTypeConfiguration<RadioChannel>
{
    public void Configure(EntityTypeBuilder<RadioChannel> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Frequency).IsRequired().HasColumnType("decimal(4, 1)");
    }
}