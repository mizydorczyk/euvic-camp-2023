namespace Core.Entities;

public class Piece : BaseEntity
{
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Version { get; set; }
    public string? PictureUrl { get; set; }

    public Artist Artist { get; set; }
    public int ArtistId { get; set; }

    public ICollection<ProgrammeItem> ProgrammeItems { get; set; } = new List<ProgrammeItem>();
}