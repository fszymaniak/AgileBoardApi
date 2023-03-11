using AgileBoard.Core.Abstractions;
using AgileBoard.Core.Contracts.Requests;
using AgileBoard.Core.Contracts.Responses;
using AgileBoard.Core.Contracts.V1;
using AgileBoard.Core.Domain;
using AgileBoard.Core.Extensions;
using AgileBoard.Core.Services;
using AgileBoard.Core.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

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
            var result = _mapper.Map<IEnumerable<UserStoryResponse>>(userStories);
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserStoryRequest? request)
        {
            var userStory = new UserStory
            (
                UserStoryId.Create(),
                new Title(request.Title.Value),
                new Owner(request.Owner.Value),
                request.Description.Value,
                new AcceptanceCriteria(request.AcceptanceCriteria.Value),
                new DefinitionOfDone(request.DefinitionOfDone.Value),
                new DefinitionOfReady(request.DefinitionOfReady.Value),
                request.Comment,
                request.StoryPoints.Value,
                request.Priority.Value,
                request.Risk.Value,
                request.Deadline.Value,
                Date.Now,
                Date.Now
            );

            //var userStory = new UserStory
            //{
            //    Id = UserStoryId.Create(),
            //    Title = request.Title.Value,
            //    Owner = request.Owner.Value,
            //    Description = request.Description.Value,
            //    AcceptanceCriteria = request.AcceptanceCriteria.Value,
            //    DefinitionOfDone = request.DefinitionOfDone.Value,
            //    DefinitionOfReady = request.DefinitionOfReady.Value,
            //    Comments = request.Comments,
            //    StoryPoints = request.StoryPoints.Value,
            //    Priority = request.Priority.Value,
            //    Risk = request.Risk.Value,
            //    Deadline = request.Deadline.Value,
            //    Created = Date.Now,
            //    Updated = Date.Now
            //};

            await _userStoryService.CreateUserStoryAsync(userStory);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.ToUriComponent();
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
