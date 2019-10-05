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
    public interface IBiddingService
    {
        Task<Bidding> Create(Bidding bidding);
        void Update(Bidding biddingParam);
    }

    public class BiddingService : IBiddingService
    {
        private readonly IBiddingRepository _biddingRepository;
        private readonly IClient _client;

        public BiddingService(IBiddingRepository biddingRepository, IClient client)
        {
            _biddingRepository = biddingRepository;
            _client = client;
        }

        public async Task<Bidding> Create(Bidding bidding)
        {
            if (bidding == null)
                throw new InvalidBidding();
            // Find if owner exists
            var user = await _client.GetUserAsync(bidding.OwnerId);
            if (user != null)
            {
                var createdBidding = _biddingRepository.Create(bidding);
                if (createdBidding != null)
                    return createdBidding;
                throw new InvalidBidding("Could not create bidding");
            }

            throw new UserNotFound();
        }

        public void Update(Bidding bidding)
        {
            if (bidding == null)
                throw new InvalidBidding();

            if (_biddingRepository.GetById(bidding.Id) != null);
            {
                _biddingRepository.Update(bidding.Id, bidding);
            }
        }
    }
}