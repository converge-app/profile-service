using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using AutoMapper;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bidding, BiddingDto>();
            CreateMap<BiddingDto, Bidding>();
            CreateMap<BiddingUpdateDto, Bidding>();
            CreateMap<BiddingCreationDto, Bidding>();
            CreateMap<BiddingContentDto, BiddingContent>();
            CreateMap<BiddingContent, BiddingContentDto>();
        }
    }
}