using Core.Entities;

namespace Core.Interfaces;

public interface IProgrammeItemRepository
{
    Task<IReadOnlyList<ProgrammeItem>> GetALlAsync();
    Task<ProgrammeItem?> GetByIdAsync(int id);
}