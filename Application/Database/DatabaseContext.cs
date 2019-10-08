using Application.Models.Entities;
using Application.Utility.Database;
using MongoDB.Driver;

namespace Application.Database
{
    public interface IDatabaseContext
    {
        IMongoCollection<Profile> Profiles { get; }

        bool IsConnectionOpen();
    }

    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IDatabaseSettings settings)
        {
            var mongoSettings = settings.GetSettings();
            var client = new MongoClient(mongoSettings);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Profile> Profiles => _database.GetCollection<Profile>("Profiles");

        public bool IsConnectionOpen()
        {
            return _database != null;
        }
    }
}