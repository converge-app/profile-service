using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using AutoMapper;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bid, BidDto>();
            CreateMap<BidDto, Bid>();
            CreateMap<BidUpdateDto, Bid>();
            CreateMap<BidCreationDto, Bid>();
        }
    }
}