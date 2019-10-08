using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models.Entities;
using Application.Repositories;
using Application.Utility.ClientLibrary;
using Application.Utility.ClientLibrary.Project;
using Application.Utility.Exception;

namespace Application.Services
{
    public interface IProfileService
    {
        Task<Profile> CreateProfile(Profile createProfile);
        Task<Profile> UpdateProfile(Profile updateProfile);
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IClient _client;

        public ProfileService(IProfileRepository profileRepository, IClient client)
        {
            _profileRepository = profileRepository;
            _client = client;
        }

        public async Task<Profile> CreateProfile(Profile createProfile)
        {
            if (await _profileRepository.UserIdExists(createProfile.UserId))
                throw new InvalidProfile("User already exists");

            if (string.IsNullOrEmpty(createProfile.ProfilePictureUrl))
            {
                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.ASCII.GetBytes(createProfile.UserId));
                    createProfile.ProfilePictureUrl =
                        "https://www.gravatar.com/avatar/" + BitConverter.ToString(result).Replace("-", "" + "").ToLower() + "?s=256&d=identicon&r=PG";
                }
            }

            var profile = await _profileRepository.Create(createProfile);
            
            try
            {
                var user = await _client.GetUserAsync(createProfile.UserId);
                if (user == null)
                    throw new UserNotFound();
            }
            catch (UserNotFound)
            {
                await _profileRepository.Remove(profile.Id);
                throw new InvalidProfile("User not found");
            }

            return profile;
        }

        public async Task<Profile> UpdateProfile(Profile updateProfile)
        {
            if (await _profileRepository.GetById(updateProfile.Id) == null)
                throw new InvalidProfile("Profile doesn't exist, create one first'");

            await _profileRepository.Update(updateProfile.Id, updateProfile);
            return await _profileRepository.GetById(updateProfile.Id);
        }
    }
}