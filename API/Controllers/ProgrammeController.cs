using API.Models;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgrammeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProgrammeItemRepository _programmeItemRepository;

    public ProgrammeController(IProgrammeItemRepository programmeItemRepository, IMapper mapper)
    {
        _programmeItemRepository = programmeItemRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProgrammeItemDto>>> GetProgrammeItems()
    {
        return Ok(_mapper.Map<List<ProgrammeItemDto>>(await _programmeItemRepository.GetALlAsync()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProgrammeItemDto>> GetProgrammeItemById([FromRoute] int id)
    {
        var programmeItem = await _programmeItemRepository.GetByIdAsync(id);
        if (programmeItem == null) return NotFound("Programme item not found");

        return Ok(_mapper.Map<ProgrammeItemDto>(programmeItem));
    }
}