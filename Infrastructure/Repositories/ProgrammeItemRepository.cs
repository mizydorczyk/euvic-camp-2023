using Core.Entities;
using Core.Interfaces;
using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProgrammeItemRepository : IProgrammeItemRepository
{
    private readonly BroadcastingDbContext _dbContext;

    public ProgrammeItemRepository(BroadcastingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ProgrammeItem>> GetALlAsync()
    {
        var programmeItems = await _dbContext.ProgrammeItems
            .Include(x => x.Piece).ThenInclude(x => x.Artist)
            .Include(x => x.RadioChannel)
            .ToListAsync();

        return programmeItems;
    }

    public async Task<ProgrammeItem?> GetByIdAsync(int id)
    {
        var programmeItem = await _dbContext.ProgrammeItems
            .Include(x => x.Piece).ThenInclude(x => x.Artist)
            .Include(x => x.RadioChannel)
            .FirstOrDefaultAsync(x => x.Id == id);

        return programmeItem;
    }
}