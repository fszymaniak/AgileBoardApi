using AgileBoard.Api.Data;
using AgileBoard.Api.Domain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Api.Services
{
    public class UserStoryService : IUserStoryService
    {
        private readonly DataContext _dataContext;

        public UserStoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UserStory>> GetUserStoriesAsync()
        {
            return await _dataContext.UserStories.ToListAsync();
        }

        public async Task<UserStory> GetUserStoryByIdAsync(Guid userStoryId)
        {
            return await _dataContext.UserStories.SingleOrDefaultAsync(x => x.Id == userStoryId);
        }

        public async Task<bool> CreateUserStoryAsync(UserStory userStory)
        {
            await _dataContext.UserStories.AddAsync(userStory);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate)
        {
            _dataContext.UserStories.Update(userStoryToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteUserStoryAsync(Guid userStoryId)
        {
            var userStory = await GetUserStoryByIdAsync(userStoryId);
            if (userStory == null)
                return false;

            _dataContext.UserStories.Remove(userStory);

            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> PatchUpdateUserStoryAsync(Guid userStoryId, JsonPatchDocument userStoryToPatchUpdate)
        {
            var userStory = await GetUserStoryByIdAsync(userStoryId);
            if (userStory == null)
                return false;

            userStoryToPatchUpdate.ApplyTo(userStory);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
