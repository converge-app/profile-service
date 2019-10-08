using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Application.Repositories
{
    public interface IProfileRepository
    {
        Task<List<Profile>> Get();
        Task<Profile> GetById(string id);
        Task<Profile> Create(Profile profile);
        Task Update(string id, Profile profileIn);
        Task Remove(Profile profileIn);
        Task Remove(string id);
        Task<bool> UserIdExists(string userId);
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

        public async Task<bool> UserIdExists(string userId)
        {
            return (await _profiles.Find(profile => profile.UserId == userId).FirstOrDefaultAsync()) != null;
        }

        public async Task<Profile> Create(Profile profile)
        {
            await _profiles.InsertOneAsync(profile);
            return profile;
        }

        public async Task Update(string id, Profile profileIn)
        {
            var profile = await GetById(id);

            if (!string.IsNullOrEmpty(profileIn.Title))
                profile.Title = profileIn.Title;
            if (!string.IsNullOrEmpty(profileIn.Description))
                profile.Description = profileIn.Description;
            if (!string.IsNullOrEmpty(profileIn.UserId))
                profile.UserId = profileIn.UserId;
            if (!string.IsNullOrEmpty(profileIn.ProfilePictureUrl))
                profile.ProfilePictureUrl = profileIn.ProfilePictureUrl;
            if (profileIn.Experience.Count > 0)
                profile.Experience = profileIn.Experience;
            if (profileIn.Skills.Count > 0)
                profile.Skills = profileIn.Skills;
            if (profileIn.Rating != -1)
                profile.Rating = profileIn.Rating;

            await _profiles.ReplaceOneAsync(p => p.Id == id, profile);
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