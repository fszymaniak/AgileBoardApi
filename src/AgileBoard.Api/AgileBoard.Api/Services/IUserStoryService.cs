using AgileBoard.Api.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace AgileBoard.Api.Services
{
    public interface IUserStoryService
    {
        Task<List<UserStory>> GetUserStoriesAsync();

        Task<UserStory> GetUserStoryByIdAsync(Guid userStoryId);

        Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> PatchUpdateUserStoryAsync(Guid userStoryId, JsonPatchDocument userStoryToPatchUpdate);

        Task<bool> CreateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> DeleteUserStoryAsync(Guid userStoryId);
    }
}
