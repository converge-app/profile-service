using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using Application.Repositories;
using Application.Utility.ClientLibrary;
using Application.Utility.ClientLibrary.Project;
using Application.Utility.Exception;
using Application.Utility.Models;
using Newtonsoft.Json;

namespace Application.Services
{
    public interface IBidService
    {
        Task<Bid> Open(Bid bid);
        Task<bool> Accept(Bid bid, string authorizationToken);
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

        public async Task<Bid> Open(Bid bid)
        {
            var project = await _client.GetProjectAsync(bid.ProjectId);
            if (project == null) throw new InvalidBid();

            var createdBid = await _bidRepository.Create(bid);

            return createdBid ?? throw new InvalidBid();
        }

        public async Task<bool> Accept(Bid bid, string authorizationToken)
        {
            var project = await _client.GetProjectAsync(bid.ProjectId);
            if (project == null) throw new InvalidBid("projectId invalid");

            project.FreelancerId = bid.FreelancerId;
            var client = _client.HttpClientFactory.CreateClient("factory");


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);

            var response =
                await client.PutAsJsonAsync($"http://projects-service.api.converge-app.net/api/projects/{project.Id}",
                    project);

            return response.IsSuccessStatusCode;

            return await _client.UpdateProjectAsync(authorizationToken, project);
        }
    }
}