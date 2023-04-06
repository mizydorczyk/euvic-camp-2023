using Core.Entities;

namespace Core.Interfaces;

public interface IPieceRepository
{
    Task<IReadOnlyList<Piece>> GetALlAsync();
    Task<Piece?> GetByIdAsync(int id);
}