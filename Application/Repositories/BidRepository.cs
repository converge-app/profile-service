using System.Collections.Generic;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IBidRepository
    {
        List<Bid> Get();
        Bid GetById(string id);
        Bid GetByOwnerId(string ownerId);
        Bid GetByFreelancerId(string freelancerId);
        Bid Create(Bid bid);
        void Update(string id, Bid bidIn);
        void Remove(Bid bidIn);
        void Remove(string id);
    }

    public class BidRepository : IBidRepository
    {
        private readonly IDatabaseContext dbContext;
        private readonly IMongoCollection<Bid> _biddings;

        public BidRepository(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            if (dbContext.IsConnectionOpen()) _biddings = dbContext.Bids;
        }

        public List<Bid> Get()
        {
            return _biddings.Find(bidding => true).ToList();
        }

        public Bid GetById(string id)
        {
            return _biddings.Find(bidding => bidding.Id == id).FirstOrDefault();
        }

        public Bid GetByOwnerId(string ownerId)
        {
            return _biddings.Find(bidding => bidding.OwnerId == ownerId).FirstOrDefault();
        }

        public Bid GetByFreelancerId(string freelancerId)
        {
            return _biddings.Find(bidding => bidding.FreelancerId == freelancerId).FirstOrDefault();
        }

        public Bid Create(Bid bid)
        {
            _biddings.InsertOne(bid);
            return bid;
        }

        public void Update(string id, Bid bidIn)
        {
            _biddings.ReplaceOne(bidding => bidding.Id == id, bidIn);
        }

        public void Remove(Bid bidIn)
        {
            _biddings.DeleteOne(bidding => bidding.Id == bidIn.Id);
        }

        public void Remove(string id)
        {
            _biddings.DeleteOne(bidding => bidding.Id == id);
        }
    }
}