using API.Models;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Piece, PieceDto>()
            .ForMember(d => d.Artist, m => m.MapFrom(s => s.Artist.KnownAs));
        CreateMap<ProgrammeItem, ProgrammeItemDto>();
        CreateMap<RadioChannel, RadioChannelDto>();
    }
}