namespace API.Models;

public class PieceDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Version { get; set; }
    public string Artist { get; set; }
    public DateTime ReleaseDate { get; set; }
    public long Views { get; set; }
    public IEnumerable<ProgrammeItemHeaderDto>? ProgrammeItemHeaders { get; set; }
}