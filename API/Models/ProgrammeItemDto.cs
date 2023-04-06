namespace API.Models;

public class ProgrammeItemDto
{
    public DateTime PlaybackDate { get; set; }
    public TimeSpan BroadcastingDuration { get; set; }
    public RadioChannelDto RadioChannel { get; set; }
    public PieceDto Piece { get; set; }
}