namespace Core.Entities;

public class RadioChannel : BaseEntity
{
    public string Name { get; set; }
    public decimal Frequency { get; set; }
    public string? PictureUrl { get; set; }
    public ICollection<ProgrammeItem> ProgrammeItems { get; set; } = new List<ProgrammeItem>();
}