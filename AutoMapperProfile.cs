using AutoMapper;
using LegoSets.Dtos.LegoSets;
using LegoSets.Models;

namespace LegoSets
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LegoSet, GetLegoSetDto>();
            CreateMap<AddLegoSetDto, LegoSet>();
            CreateMap<UpdateLegoSetDto, LegoSet>();
        }
    }
}
