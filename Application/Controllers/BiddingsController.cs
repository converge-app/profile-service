using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using Application.Utility;
using Application.Utility.Exception;
using Application.Utility.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BiddingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly IBidService _bidService;

        public BiddingsController(IBidService bidService, IBidRepository bidRepository, IMapper mapper)
        {
            _bidService = bidService;
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] BidCreationDto bidDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var createBid = _mapper.Map<Bid>(bidDto);
            try
            {
                var createdBid = await _bidService.Create(createBid);
                return Ok(createdBid);
            }
            catch (UserNotFound)
            {
                return NotFound(new MessageObj("User not found"));
            }
            catch (EnvironmentNotSet)
            {
                throw;
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var biddings = _bidRepository.Get();
            var biddingDtos = _mapper.Map<IList<BidDto>>(biddings);
            return Ok(biddingDtos);
        }

        [HttpGet("employer/{id}")]
        [AllowAnonymous]
        public IActionResult GetByOwnerId(string id)
        {
            var bidding = _bidRepository.GetByOwnerId(id);
            var bidDto = _mapper.Map<BidDto>(bidding);
            return Ok(bidDto);
        }

        [HttpGet("freelancer/{id}")]
        [AllowAnonymous]
        public IActionResult GetByFreelancerId(string id)
        {
            var bid = _bidRepository.GetByFreelancerId(id);
            var bidDto = _mapper.Map<BidDto>(bid);
            return Ok(bidDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(string id)
        {
            var bid = _bidRepository.GetById(id);
            var bidDto = _mapper.Map<BidDto>(bid);
            return Ok(bidDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] BidUpdateDto bidDto)
        {
            var bidding = _mapper.Map<Bid>(bidDto);
            bidding.Id = id;

            try
            {
                _bidService.Update(bidding);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _bidRepository.Remove(id);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }

            return Ok();
        }
    }
}