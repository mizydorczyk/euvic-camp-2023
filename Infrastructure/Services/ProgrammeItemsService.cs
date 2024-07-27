using Core.Entities;
using Core.Interfaces;
using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Infrastructure.Repositories;

public class ProgrammeItemsService : IProgrammeItemsRepository
{
    private readonly BroadcastingDbContext _dbContext;
    private readonly ISieveProcessor _sieveProcessor;

    public ProgrammeItemsService(BroadcastingDbContext dbContext, ISieveProcessor sieveProcessor)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<(List<ProgrammeItem> Items, int TotalCount)> ListAsync(SieveModel query)
    {
        var programmeItems = _dbContext.ProgrammeItems
            .Include(x => x.RadioChannel)
            .Include(x => x.Piece).ThenInclude(x => x.Artist)
            .AsQueryable();

        var programmeItemsToReturn = await _sieveProcessor.Apply(query, programmeItems).ToListAsync();
        var totalCount = await _sieveProcessor.Apply(query, programmeItems, applyPagination: false).CountAsync();

        return (programmeItemsToReturn, totalCount);
    }
}