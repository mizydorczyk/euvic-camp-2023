using Core.Entities;
using Core.Interfaces;
using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PieceService : IPieceRepository
{
    private readonly BroadcastingDbContext _dbContext;

    public PieceService(BroadcastingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Piece>> ListTopAsync(int positions)
    {
        var pieces = await _dbContext.Pieces
            .Include(x => x.Artist)
            .Include(x => x.ProgrammeItems)
            .OrderByDescending(x => x.ProgrammeItems.Sum(y => y.Views))
            .Take(positions)
            .ToListAsync();

        return pieces;
    }

    public async Task<Piece?> GetByIdAsync(int id)
    {
        var piece = await _dbContext.Pieces
            .Include(x => x.Artist)
            .Include(x => x.ProgrammeItems).ThenInclude(x => x.RadioChannel)
            .FirstOrDefaultAsync(x => x.Id == id);

        return piece;
    }
}