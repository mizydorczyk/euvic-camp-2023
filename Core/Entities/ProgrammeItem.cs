namespace Core.Entities;

public class ProgrammeItem : BaseEntity
{
    public DateTime PlaybackDate { get; set; }
    public TimeSpan BroadcastingDuration { get; set; }
    public long Views { get; set; }

    public RadioChannel RadioChannel { get; set; }
    public int RadioChannelId { get; set; }

    public Piece Piece { get; set; }
    public int PieceId { get; set; }
}