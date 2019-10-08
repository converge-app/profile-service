using Application.Models.DataTransferObjects;
using Profile = AutoMapper.Profile;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
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