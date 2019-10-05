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
        private readonly IBiddingRepository _biddingRepository;
        private readonly IBiddingService _biddingService;

        public BiddingsController(IBiddingService biddingService, IBiddingRepository biddingRepository, IMapper mapper)
        {
            _biddingService = biddingService;
            _biddingRepository = biddingRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBidding([FromBody] BiddingCreationDto biddingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var createBidding = _mapper.Map<Bidding>(biddingDto);
            try
            {
                var createdBidding = await _biddingService.Create(createBidding);
                return Ok(createdBidding);
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
            var biddings = _biddingRepository.Get();
            var biddingDtos = _mapper.Map<IList<BiddingDto>>(biddings);
            return Ok(biddingDtos);
        }

        [HttpGet("employer/{id}")]
        [AllowAnonymous]
        public IActionResult GetByOwnerId(string id)
        {
            var bidding = _biddingRepository.GetByOwnerId(id);
            var biddingDto = _mapper.Map<BiddingDto>(bidding);
            return Ok(biddingDto);
        }

        [HttpGet("freelancer/{id}")]
        [AllowAnonymous]
        public IActionResult GetByFreelancerId(string id)
        {
            var bidding = _biddingRepository.GetByFreelancerId(id);
            var biddingDto = _mapper.Map<BiddingDto>(bidding);
            return Ok(biddingDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(string id)
        {
            var bidding = _biddingRepository.GetById(id);
            var biddingDto = _mapper.Map<BiddingDto>(bidding);
            return Ok(biddingDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] BiddingUpdateDto biddingDto)
        {
            var bidding = _mapper.Map<Bidding>(biddingDto);
            bidding.Id = id;

            try
            {
                _biddingService.Update(bidding);
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
                _biddingRepository.Remove(id);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }

            return Ok();
        }
    }
}