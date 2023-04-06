using API.Models;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Piece, PieceDto>()
            .ForMember(d => d.Artist, m => m.MapFrom(s => s.Artist.KnownAs))
            .ForMember(d => d.Views, m => m.MapFrom(s => s.ProgrammeItems.Sum(x => x.Views)));
        CreateMap<Piece, PieceWithProgrammeItemsDto>()
            .ForMember(d => d.Artist, m => m.MapFrom(s => s.Artist.KnownAs))
            .ForMember(d => d.Views, m => m.MapFrom(s => s.ProgrammeItems.Sum(x => x.Views)));
        CreateMap<ProgrammeItem, ProgrammeItemDto>();
        CreateMap<ProgrammeItem, ProgrammeItemWithPieceDto>();
        CreateMap<RadioChannel, RadioChannelDto>();
    }
}