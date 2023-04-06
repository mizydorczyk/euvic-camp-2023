namespace API.Models;

public class PieceWithProgrammeItemsDto : PieceDto
{
    public List<ProgrammeItemDto> ProgrammeItems { get; set; }
}