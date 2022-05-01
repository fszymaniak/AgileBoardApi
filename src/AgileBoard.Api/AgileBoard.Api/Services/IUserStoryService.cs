using AgileBoard.Api.Domain;

namespace AgileBoard.Api.Services
{
    public interface IUserStoryService
    {
        Task<List<UserStory>> GetUserStoriesAsync();

        Task<UserStory> GetUserStoryByIdAsync(Guid userStoryId);

        Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> CreateUserStoryAsync(UserStory userStoryToUpdate);

        Task<bool> DeleteUserStoryAsync(Guid userStoryId);
    }
}
