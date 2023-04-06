using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Broadcasting;

public static class BroadcastingDbContextSeed
{
    public static async Task SeedAsync(BroadcastingDbContext context)
    {
        var projectPath = Directory.GetParent(Environment.CurrentDirectory)?.FullName;

        if (!context.Artists.Any())
        {
            var artistsData = await File.ReadAllTextAsync(projectPath + @"/Infrastructure/Broadcasting/SeedData/artists.json");
            var artists = JsonSerializer.Deserialize<List<Artist>>(artistsData);
            if (artists == null) throw new Exception("artists.json is empty or deserialization has gone terribly wrong");
            context.Artists.AddRange(artists);
        }

        if (!context.RadioChannels.Any())
        {
            var channelsData = await File.ReadAllTextAsync(projectPath + @"/Infrastructure/Broadcasting/SeedData/channels.json");
            var channels = JsonSerializer.Deserialize<List<RadioChannel>>(channelsData);
            if (channels == null) throw new Exception("channels.json is empty or deserialization has gone terribly wrong");
            context.RadioChannels.AddRange(channels);
        }

        if (!context.Pieces.Any() && !context.ProgrammeItems.Any())
        {
            var random = new Random();

            var piecesData = await File.ReadAllTextAsync(projectPath + @"/Infrastructure/Broadcasting/SeedData/pieces.json");
            var pieces = JsonSerializer.Deserialize<List<Piece>>(piecesData);
            if (pieces == null) throw new Exception("pieces.json is empty or deserialization has gone terribly wrong");
            foreach (var piece in pieces)
            {
                piece.ReleaseDate = DateTime.UtcNow.AddDays(-random.Next(365));
                piece.Duration = TimeSpan.FromSeconds(random.Next(120, 300));
            }

            context.Pieces.AddRange(pieces);

            var programmeItemsData = await File.ReadAllTextAsync(projectPath + @"/Infrastructure/Broadcasting/SeedData/programmeItems.json");
            var programmeItems = JsonSerializer.Deserialize<List<ProgrammeItem>>(programmeItemsData);
            if (programmeItems == null) throw new Exception("programItems.json is empty or deserialization has gone terribly wrong");
            foreach (var programmeItem in programmeItems)
            {
                programmeItem.Views = random.Next();
                programmeItem.PlaybackDate = DateTime.UtcNow - TimeSpan.FromHours(random.Next(1, 100));
                var piece = pieces.FirstOrDefault(x => x.Id == programmeItem.PieceId);
                if (piece == null)
                {
                    programmeItem.BroadcastingDuration = TimeSpan.FromSeconds(random.Next(120, 300));
                    ;
                }
                else
                {
                    programmeItem.BroadcastingDuration = piece.Duration;
                }
            }

            context.ProgrammeItems.AddRange(programmeItems);
        }

        await context.SaveChangesAsync();
    }
}