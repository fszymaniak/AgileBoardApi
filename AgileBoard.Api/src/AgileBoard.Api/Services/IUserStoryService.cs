using AgileBoard.Api.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace AgileBoard.Api.Services
{
    public interface IUserStoryService
    {
        Task<IEnumerable<UserStory>> GetUserStoriesAsync();

        Task<UserStory?> GetUserStoryByIdAsync(Guid userStoryId);

        Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> PatchUpdateUserStoryAsync(Guid userStoryId, JsonPatchDocument userStoryToPatchUpdate);

        Task<bool> CreateUserStoryAsync(UserStory userStoryToCreate);

        Task<bool> DeleteUserStoryAsync(Guid userStoryId);
    }
}
