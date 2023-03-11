using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace AgileBoard.Core.Repositories
{
    public interface IUserStoryRepository
    {
        Task<IEnumerable<UserStory>> GetUserStoriesAsync();

        Task<UserStory> GetUserStoryByIdAsync(UserStoryId userStoryId);

        Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> PatchUpdateUserStoryAsync(UserStoryId userStoryId, JsonPatchDocument userStoryToPatchUpdate);

        Task<bool> CreateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> DeleteUserStoryAsync(UserStoryId userStoryId);
    }
}