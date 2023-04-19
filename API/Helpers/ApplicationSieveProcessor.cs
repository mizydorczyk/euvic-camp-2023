using Core.Entities;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace API.Helpers;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<ProgrammeItem>(x => x.PlaybackDate)
            .CanSort();

        mapper.Property<ProgrammeItem>(x => x.RadioChannel.Name)
            .HasName("RadioChannelName")
            .CanSort();

        mapper.Property<ProgrammeItem>(x => x.BroadcastingDuration)
            .CanSort();

        mapper.Property<ProgrammeItem>(x => x.Piece.Title)
            .HasName("Title")
            .CanSort()
            .CanFilter();

        mapper.Property<ProgrammeItem>(x => x.Piece.Artist.KnownAs)
            .HasName("Artist")
            .CanSort();

        return mapper;
    }
}