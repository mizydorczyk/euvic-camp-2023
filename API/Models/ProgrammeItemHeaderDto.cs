namespace API.Models;

public class ProgrammeItemHeaderDto
{
    public DateTime PlaybackDate { get; set; }
    public TimeSpan BroadcastingDuration { get; set; }
    public RadioChannelDto RadioChannel { get; set; }
    public long Views { get; set; }
}