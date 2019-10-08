using Application.Models.DataTransferObjects;
using Application.Models.Entities;

namespace Application.Helpers
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Profile, ProfileDto>();
            CreateMap<ProfileDto, Profile>();
            CreateMap<ProfileUpdateDto, Profile>();
            CreateMap<ProfileCreationDto, Profile>();
        }
    }
}