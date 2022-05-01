﻿using AgileBoard.Api.Contracts.Requests;
using AgileBoard.Api.Contracts.Responses;
using AgileBoard.Api.Contracts.V1;
using AgileBoard.Api.Domain;
using AgileBoard.Api.Extensions;
using AgileBoard.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class UserStoriesController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;
        private readonly IMapper _mapper;

        public UserStoriesController(IUserStoryService userStoryService, IMapper mapper)
        {
            _userStoryService = userStoryService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.UserStories.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var userStories = await _userStoryService.GetUserStoriesAsync();
            return Ok(_mapper.Map<List<UserStoryResponse>>(userStories));
        }

        [HttpGet(ApiRoutes.UserStories.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid userStoryId)
        {
            var userStory = await _userStoryService.GetUserStoryByIdAsync(userStoryId);
            if(userStory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserStoryResponse>(userStory));
        }

        [HttpPost(ApiRoutes.UserStories.Create)]
        public async Task<IActionResult> Create([FromBody] CreateUserStoryRequest request)
        {
            var userStory = new UserStory
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Owner = request.Owner,
                Description = request.Description,
                AcceptanceCriteria = request.AcceptanceCriteria,
                DefinitionOfDone = request.DefinitionOfDone,
                DefintionOfReady = request.DefintionOfReady,
                Comments = request.Comments,
                StoryPoints = request.StoryPoints,
                Priority = request.Priority,
                Risk = request.Risk,
                Deadline = request.Deadline,
                Created = DateTime.UtcNow,
                Updated = null
            };

            await _userStoryService.CreateUserStoryAsync(userStory);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.UserStories.Get.Replace("{userStoryId}", userStory.Id.ToString());

            return Created(locationUri, _mapper.Map<UserStoryResponse>(userStory));
        }

        [HttpPut(ApiRoutes.UserStories.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid userStoryId, [FromBody] UpdateUserStoryRequest request)
        {
            var userStory = await _userStoryService.GetUserStoryByIdAsync(userStoryId);
            userStory.UpdateUserStory(request);


            var updated = await _userStoryService.UpdateUserStoryAsync(userStory);

            if (updated)
                return Ok(_mapper.Map<UserStoryResponse>(userStory));

            return NotFound();
        }

        [HttpDelete(ApiRoutes.UserStories.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid userStoryId)
        {
            var deleted =  await _userStoryService.DeleteUserStoryAsync(userStoryId);

            if(deleted)
                return NoContent();

            return NotFound();
        }
    }
}
