using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
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
using Profile = Application.Models.Entities.Profile;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProfilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService, IProfileRepository profileRepository, IMapper mapper)
        {
            _profileService = profileService;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfileCreationDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var createProfile = _mapper.Map<Profile>(profileDto);
            try
            {
                var profile = await _profileService.CreateProfile(createProfile);
                return Ok(profile);
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

        [HttpPut("{profileId}")]
        public async Task<IActionResult> Update([FromRoute] string profileId, [FromBody] ProfileUpdateDto profileDto)
        {
            if (profileId != profileDto.Id)
                return BadRequest(new MessageObj("Invalid id(s)"));

            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var updateProfile = _mapper.Map<Profile>(profileDto);
            try
            {
                Profile profile = await _profileService.UpdateProfile(updateProfile);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profileRepository.Get();
            var profileDtos = _mapper.Map<IList<ProfileDto>>(profiles);
            return Ok(profileDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var profile = await _profileRepository.GetById(id);
            var profileDto = _mapper.Map<ProfileDto>(profile);
            return Ok(profileDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _profileRepository.Remove(id);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageObj(e.Message));
            }

            return Ok();
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserId([FromRoute] string userId)
        {
            var profile = await _profileRepository.GetByUserId(userId);
            if (profile != null)
                return Ok(profile);
            return NotFound();
        }
    }
}