using System.Collections.Generic;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IBiddingRepository
    {
        List<Bidding> Get();
        Bidding GetById(string id);
        Bidding GetByOwnerId(string ownerId);
        Bidding GetByFreelancerId(string freelancerId);
        Bidding Create(Bidding bidding);
        void Update(string id, Bidding biddingIn);
        void Remove(Bidding biddingIn);
        void Remove(string id);
    }

    public class BiddingRepository : IBiddingRepository
    {
        private readonly IDatabaseContext dbContext;
        private readonly IMongoCollection<Bidding> _biddings;

        public BiddingRepository(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            if (dbContext.IsConnectionOpen()) _biddings = dbContext.Biddings;
        }

        public List<Bidding> Get()
        {
            return _biddings.Find(bidding => true).ToList();
        }

        public Bidding GetById(string id)
        {
            return _biddings.Find(bidding => bidding.Id == id).FirstOrDefault();
        }

        public Bidding GetByOwnerId(string ownerId)
        {
            return _biddings.Find(bidding => bidding.OwnerId == ownerId).FirstOrDefault();
        }

        public Bidding GetByFreelancerId(string freelancerId)
        {
            return _biddings.Find(bidding => bidding.FreelancerId == freelancerId).FirstOrDefault();
        }

        public Bidding Create(Bidding bidding)
        {
            _biddings.InsertOne(bidding);
            return bidding;
        }

        public void Update(string id, Bidding biddingIn)
        {
            _biddings.ReplaceOne(bidding => bidding.Id == id, biddingIn);
        }

        public void Remove(Bidding biddingIn)
        {
            _biddings.DeleteOne(bidding => bidding.Id == biddingIn.Id);
        }

        public void Remove(string id)
        {
            _biddings.DeleteOne(bidding => bidding.Id == id);
        }
    }
}