namespace Core.Entities;

public class Artist : BaseEntity
{
    public string KnownAs { get; set; }
    public string? PictureUrl { get; set; }

    public ICollection<Piece> Pieces { get; set; } = new List<Piece>();
}