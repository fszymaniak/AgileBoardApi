using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace AgileBoard.Core.Services
{
    public interface IUserStoryService
    {
        Task<IEnumerable<UserStory>> GetUserStoriesAsync();

        Task<UserStory?> GetUserStoryByIdAsync(UserStoryId userStoryId);

        Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> PatchUpdateUserStoryAsync(UserStoryId userStoryId, JsonPatchDocument userStoryToPatchUpdate);

        Task<bool> CreateUserStoryAsync(UserStory userStoryToCreate);

        Task<bool> DeleteUserStoryAsync(UserStoryId userStoryId);
    }
}
