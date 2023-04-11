using Core.Entities;
using Sieve.Models;

namespace Core.Interfaces;

public interface IProgrammeItemsRepository
{
    public Task<(List<ProgrammeItem> Items, int TotalCount)> ListAsync(SieveModel query);
}