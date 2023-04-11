using Core.Entities;

namespace Core.Interfaces;

public interface IPieceRepository
{
    public Task<List<Piece>> ListTop100Async();
    public Task<Piece?> GetByIdAsync(int id);
}