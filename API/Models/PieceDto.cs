namespace API.Models;

public class PieceDto
{
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public long Views { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Version { get; set; }
    public string? PictureUrl { get; set; }
    public string Artist { get; set; }
}