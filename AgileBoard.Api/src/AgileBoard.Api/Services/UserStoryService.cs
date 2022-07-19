using AgileBoard.Api.Domain;
using AgileBoard.Api.Logging;
using AgileBoard.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace AgileBoard.Api.Services
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepository _userStoryRepository;
        private readonly ILoggerAdapter<UserStoryService> _logger;

        public UserStoryService(IUserStoryRepository userStoryRepository, ILoggerAdapter<UserStoryService> logger)
        {
            _userStoryRepository = userStoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserStory>> GetUserStoriesAsync()
        {
            _logger.LogInformation("Retrieving all user stories");
            try
            {
                return await _userStoryRepository.GetUserStoriesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving all user stories");
                throw;
            }
        }

        public async Task<UserStory?> GetUserStoryByIdAsync(Guid userStoryId)
        {
            _logger.LogInformation("Retrieving user with id: {0}", userStoryId);
            try
            {
                return await _userStoryRepository.GetUserStoryByIdAsync(userStoryId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving user story with id {0}", userStoryId);
                throw;
            }
        }

        public async Task<bool> CreateUserStoryAsync(UserStory userStoryToCreate)
        {
            _logger.LogInformation("Creating user story with id {0} and title: {1}", userStoryToCreate.Id, userStoryToCreate.Title);
            try
            {
                return await _userStoryRepository.CreateUserStoryAsync(userStoryToCreate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while creating a user story");
                throw;
            }
        }

        public async Task<bool> DeleteUserStoryAsync(Guid userStoryId)
        {
            _logger.LogInformation("Deleting user with id: {0}", userStoryId);
            try
            {
                return await _userStoryRepository.DeleteUserStoryAsync(userStoryId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while deleting user story with id {0}", userStoryId);
                throw;
            }
        }

        public async Task<bool> UpdateUserStoryAsync(UserStory userStoryToUpdate)
        {
            _logger.LogInformation("Update user with id: {0}", userStoryToUpdate.Id);
            try
            {
                return await _userStoryRepository.UpdateUserStoryAsync(userStoryToUpdate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while update user story with id {0}", userStoryToUpdate);
                throw;
            }
        }

        public async Task<bool> PatchUpdateUserStoryAsync(Guid userStoryId, JsonPatchDocument userStoryToPatchUpdate)
        {
            _logger.LogInformation("Patch update user with id: {0}", userStoryId);
            try
            {
                return await _userStoryRepository.PatchUpdateUserStoryAsync(userStoryId, userStoryToPatchUpdate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while patch update user story with id {0}", userStoryId);
                throw;
            }
        }
    }
}
