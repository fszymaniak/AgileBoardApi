using AgileBoard.Api.Clock;
using AgileBoard.Api.Contracts.Requests;
using AgileBoard.Api.Contracts.Responses;
using AgileBoard.Api.Contracts.V1;
using AgileBoard.Api.Domain;
using AgileBoard.Api.Extensions;
using AgileBoard.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers.V1
{
    [ApiController]
    public class UserStoriesController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;
        private readonly IMapper _mapper;
        private readonly IClock _clock;

        public UserStoriesController(IUserStoryService userStoryService, IMapper mapper, IClock clock)
        {
            _userStoryService = userStoryService;
            _mapper = mapper;
            _clock = clock;
        }

        [HttpGet(ApiRoutes.UserStories.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var userStories = await _userStoryService.GetUserStoriesAsync();
            //var result = userStories.Select(x => x.ToUserStoryResponse());
            var result = userStories.Select(_mapper.Map<IEnumerable<UserStoryResponse>>);
            //var result = _mapper.Map<IList<UserStory>>(userStories);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.UserStories.Get)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid userStoryId)
        {
            var userStory = await _userStoryService.GetUserStoryByIdAsync(userStoryId);
            if(userStory == null)
            {
                throw new KeyNotFoundException("User Story cannot be found");
            }

            return Ok(_mapper.Map<UserStoryResponse>(userStory));
        }

        [HttpPost(ApiRoutes.UserStories.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserStoryRequest request)
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
                Created = _clock.DateTimeNow,
                Updated = null
            };

            await _userStoryService.CreateUserStoryAsync(userStory);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.UserStories.Get.Replace("{userStoryId}", userStory.Id.ToString());

            return Created(locationUri, _mapper.Map<UserStoryResponse>(userStory));
        }

        [HttpPut(ApiRoutes.UserStories.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid userStoryId, [FromBody] UpdateUserStoryRequest request)
        {
            var userStory = await _userStoryService.GetUserStoryByIdAsync(userStoryId);
            userStory.UpdateUserStory(request);


            var updated = await _userStoryService.UpdateUserStoryAsync(userStory);

            if (updated)
                return Ok(_mapper.Map<UserStoryResponse>(userStory));

            throw new KeyNotFoundException("Updated User Story cannot be found");
        }

        [HttpPatch(ApiRoutes.UserStories.PatchUpdate)]
        public async Task<IActionResult> PatchUpdateAsync([FromRoute] Guid userStoryId, [FromBody] JsonPatchDocument request)
        {
            var updated = await _userStoryService.PatchUpdateUserStoryAsync(userStoryId, request);

            if (updated)
            {
                var userStory = await _userStoryService.GetUserStoryByIdAsync(userStoryId);
                return Ok(_mapper.Map<UserStoryResponse>(userStory));
            }

            throw new KeyNotFoundException("User Story for PATCH update cannot be found");
        }

        [HttpDelete(ApiRoutes.UserStories.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid userStoryId)
        {
            var deleted =  await _userStoryService.DeleteUserStoryAsync(userStoryId);

            if(deleted)
                return NoContent();

            throw new KeyNotFoundException("User Story for deletion cannot be found");
        }
    }
}
