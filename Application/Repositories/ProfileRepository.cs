using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IProfileRepository
    {
        Task<List<Profile>> Get();
        Task<Profile> GetById(string id);
        Task<List<Profile>> GetByProject(string projectId);
        Task<List<Profile>> GetByFreelancerId(string freelancerId);
        Task<List<Profile>> GetByProjectAndFreelancer(string projectId, string freelancerId);
        Task<Profile> Create(Profile profile);
        Task Update(string id, Profile profileIn);
        Task Remove(Profile profileIn);
        Task Remove(string id);
    }

    public class ProfileRepository : IProfileRepository
    {
        private readonly IMongoCollection<Profile> _profiles;

        public ProfileRepository(IDatabaseContext dbContext)
        {
            if (dbContext.IsConnectionOpen())
                _profiles = dbContext.Profiles;
        }

        public async Task<List<Profile>> Get()
        {
            return await (await _profiles.FindAsync(profile => true)).ToListAsync();
        }

        public async Task<Profile> GetById(string id)
        {
            return await (await _profiles.FindAsync(profiles => profiles.Id == id)).FirstOrDefaultAsync();
        }

        public async Task<List<Profile>> GetByProject(string projectId)
        {
            return await (await _profiles.FindAsync(profile => profile.ProjectId == projectId)).ToListAsync();
        }

        public async Task<List<Profile>> GetByFreelancerId(string freelancerId)
        {
            return await (await _profiles.FindAsync(profile => profile.FreelancerId == freelancerId)).ToListAsync();
        }

        public async Task<List<Profile>> GetByProjectAndFreelancer(string projectId, string freelancerId)
        {
            return await (
                await _profiles.FindAsync(
                    profile => profile.ProjectId == projectId && profile.FreelancerId == freelancerId)
            ).ToListAsync();
        }

        public async Task<Profile> Create(Profile profile)
        {
            await _profiles.InsertOneAsync(profile);
            return profile;
        }

        public async Task Update(string id, Profile profileIn)
        {
            await _profiles.ReplaceOneAsync(profiles => profiles.Id == id, profileIn);
        }

        public async Task Remove(Profile profileIn)
        {
            await _profiles.DeleteOneAsync(profiles => profiles.Id == profileIn.Id);
        }

        public async Task Remove(string id)
        {
            await _profiles.DeleteOneAsync(profiles => profiles.Id == id);
        }
    }
}