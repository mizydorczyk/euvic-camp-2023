using Core.Entities;
using Infrastructure.Broadcasting.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Broadcasting;

public class BroadcastingDbContext : DbContext
{
    public BroadcastingDbContext(DbContextOptions<BroadcastingDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Piece> Pieces { get; set; }
    public DbSet<ProgrammeItem> ProgrammeItems { get; set; }
    public DbSet<RadioChannel> RadioChannels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new PieceConfiguration());
        modelBuilder.ApplyConfiguration(new ProgrammeConfiguration());
        modelBuilder.ApplyConfiguration(new RadioChannelConfiguration());
    }
}