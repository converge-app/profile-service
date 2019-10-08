using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models.Entities;
using Application.Repositories;
using Application.Utility.ClientLibrary;
using Application.Utility.ClientLibrary.Project;

namespace Application.Services
{
    public interface IProfileService
    {
        Task<Profile> Open(Profile profile);
        Task<bool> Accept(Profile profile, string authorizationToken);
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

        public async Task<Profile> Open(Profile profile)
        {
            var project = await _client.GetProjectAsync(profile.ProjectId);
            if (project == null)throw new InvalidProfile();

            var createdProfile = await _profileRepository.Create(profile);

            return createdProfile ??
                throw new InvalidProfile();
        }

        public async Task<bool> Accept(Profile profile, string authorizationToken)
        {
            var project = await _client.GetProjectAsync(profile.ProjectId);
            if (project == null)throw new InvalidProfile("projectId invalid");

            project.FreelancerId = profile.FreelancerId;

            return await _client.UpdateProjectAsync(authorizationToken, project);
        }
    }
}