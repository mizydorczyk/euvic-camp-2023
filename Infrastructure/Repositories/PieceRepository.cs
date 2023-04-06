using Core.Entities;
using Core.Interfaces;
using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PieceRepository : IPieceRepository
{
    private readonly BroadcastingDbContext _dbContext;

    public PieceRepository(BroadcastingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Piece>> GetALlAsync()
    {
        var pieces = await _dbContext.Pieces
            .Include(x => x.Artist)
            .Include(x => x.ProgrammeItems)
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