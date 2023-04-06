using API.Models;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]
public class PieceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPieceRepository _pieceRepository;

    public PieceController(IPieceRepository pieceRepository, IMapper mapper)
    {
        _pieceRepository = pieceRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PieceDto>>> GetPieces()
    {
        return Ok(_mapper.Map<List<PieceDto>>(await _pieceRepository.GetALlAsync()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PieceDto>> GetPieceById([FromRoute] int id)
    {
        var piece = await _pieceRepository.GetByIdAsync(id);
        if (piece == null) return NotFound("Piece not found");

        return Ok(_mapper.Map<PieceDto>(piece));
    }
}