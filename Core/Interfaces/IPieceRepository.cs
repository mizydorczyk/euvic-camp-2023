using Core.Entities;

namespace Core.Interfaces;

public interface IPieceRepository
{
    public Task<List<Piece>> ListTopAsync(int positions);
    public Task<Piece?> GetByIdAsync(int id);
}