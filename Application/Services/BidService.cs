using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using Application.Repositories;
using Application.Utility.ClientLibrary;
using Application.Utility.Exception;
using Application.Utility.Models;

namespace Application.Services
{
    public interface IBidService
    {
        Task<Bid> Create(Bid bid);
        void Update(Bid bidParam);
    }

    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IClient _client;

        public BidService(IBidRepository bidRepository, IClient client)
        {
            _bidRepository = bidRepository;
            _client = client;
        }

        public async Task<Bid> Create(Bid bid)
        {
            if (bid == null)
                throw new InvalidBid();
            // Find if owner exists
            var user = await _client.GetUserAsync(bid.OwnerId);
            if (user != null)
            {
                var createdBidding = _bidRepository.Create(bid);
                if (createdBidding != null)
                    return createdBidding;
                throw new InvalidBid("Could not create bidding");
            }

            throw new UserNotFound();
        }

        public void Update(Bid bid)
        {
            if (bid == null)
                throw new InvalidBid();

            if (_bidRepository.GetById(bid.Id) != null);
            {
                _bidRepository.Update(bid.Id, bid);
            }
        }
    }
}