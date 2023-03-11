using AgileBoard.Core.Domain;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using AgileBoard.Infrastructure.DAL;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.Repositories
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly AgileBoardDbContext _AgileBoardDbContext;

        public UserStoryRepository(AgileBoardDbContext AgileBoardDbContext)
        {
            _AgileBoardDbContext = AgileBoardDbContext;
        }

        public async Task<IEnumerable<UserStory>> GetUserStoriesAsync()
        {
            return await _AgileBoardDbContext.UserStories.ToListAsync();
        }

        public async Task<UserStory> GetUserStoryByIdAsync(UserStoryId userStoryId)
        {
            return await _AgileBoardDbContext.UserStories.SingleOrDefaultAsync(x => x.Id.Equals(userStoryId.Value));
        }

        public async Task<bool> CreateUserStoryAsync(UserStory userStory)
        {
            await _AgileBoardDbContext.UserStories.AddAsync(userStory);
            var created = await _AgileBoardDbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate)
        {
            _AgileBoardDbContext.UserStories.Update(userStoryToUpdate);
            var updated = await _AgileBoardDbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteUserStoryAsync(UserStoryId userStoryId)
        {
            var userStory = await GetUserStoryByIdAsync(userStoryId);
            if (userStory == null)
                return false;

            _AgileBoardDbContext.UserStories.Remove(userStory);

            var deleted = await _AgileBoardDbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> PatchUpdateUserStoryAsync(UserStoryId userStoryId, JsonPatchDocument userStoryToPatchUpdate)
        {
            var userStory = await GetUserStoryByIdAsync(userStoryId);
            if (userStory == null)
                return false;

            userStoryToPatchUpdate.ApplyTo(userStory);
            var updated = await _AgileBoardDbContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
