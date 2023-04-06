namespace API.Models;

public class ProgrammeItemWithPieceDto : ProgrammeItemDto
{
    public PieceDto Piece { get; set; }
}