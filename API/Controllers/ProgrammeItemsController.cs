using API.Helpers;
using API.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public class ProgrammeItemsController : ControllerBase
{
    private readonly IProgrammeItemsRepository _programmeItemsRepository;

    public ProgrammeItemsController(IProgrammeItemsRepository programmeItemsRepository)
    {
        _programmeItemsRepository = programmeItemsRepository;
    }

    [Cache(300)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProgrammeItemDto>>> GetAllProgrammeItems([FromQuery] SieveModel query)
    {
        var result = await _programmeItemsRepository.ListAsync(query);
        var programmeItems = result.Items.Select(programmeItem => new ProgrammeItemDto
        {
            PlaybackDate = programmeItem.PlaybackDate,
            BroadcastingDuration = programmeItem.BroadcastingDuration,
            RadioChannel = new RadioChannelDto
            {
                Name = programmeItem.RadioChannel.Name,
                Frequency = programmeItem.RadioChannel.Frequency
            },
            Piece = new PieceDto
            {
                Id = programmeItem.Piece.Id,
                Title = programmeItem.Piece.Title,
                ReleaseDate = programmeItem.Piece.ReleaseDate,
                Duration = programmeItem.Piece.Duration,
                Version = programmeItem.Piece.Version,
                Artist = programmeItem.Piece.Artist.KnownAs,
                Views = programmeItem.Piece.ProgrammeItems.Sum(x => x.Views)
            },
            Views = programmeItem.Views
        }).ToList();

        return Ok(new PagedResult<ProgrammeItemDto>(programmeItems, result.TotalCount, query.PageSize.Value, query.Page.Value));
    }
}