using API.Helpers;
using API.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public class PieceController : ControllerBase
{
    private readonly IPieceRepository _pieceRepository;

    public PieceController(IPieceRepository pieceRepository)
    {
        _pieceRepository = pieceRepository;
    }

    [Cache(300)]
    [HttpGet("top100")]
    public async Task<ActionResult<List<PieceDto>>> GetTop100Pieces()
    {
        var pieces = await _pieceRepository.ListTop100Async();

        return Ok(pieces.Select(piece => new PieceDto
        {
            Id = piece.Id,
            Title = piece.Title,
            ReleaseDate = piece.ReleaseDate,
            Views = piece.ProgrammeItems.Sum(x => x.Views),
            Duration = piece.Duration,
            Version = piece.Version,
            Artist = piece.Artist.KnownAs
        }));
    }

    [Cache(300)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PieceDto>> GetPiece([FromRoute] int id)
    {
        var piece = await _pieceRepository.GetByIdAsync(id);
        if (piece == null) return NotFound("Piece not found");

        return Ok(new PieceDto
        {
            Id = piece.Id,
            Title = piece.Title,
            ReleaseDate = piece.ReleaseDate,
            Views = piece.ProgrammeItems.Sum(x => x.Views),
            Duration = piece.Duration,
            Version = piece.Version,
            Artist = piece.Artist.KnownAs,
            ProgrammeItemHeaders = piece.ProgrammeItems.OrderBy(x => x.PlaybackDate).Select(programmeItem => new ProgrammeItemHeaderDto
            {
                PlaybackDate = programmeItem.PlaybackDate,
                BroadcastingDuration = programmeItem.BroadcastingDuration,
                RadioChannel = new RadioChannelDto
                {
                    Name = programmeItem.RadioChannel.Name,
                    Frequency = programmeItem.RadioChannel.Frequency
                },
                Views = programmeItem.Views
            })
        });
    }
}