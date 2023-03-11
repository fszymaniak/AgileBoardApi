using AgileBoard.Core.Contracts.Responses;
using AgileBoard.Core.Domain;

namespace AgileBoard.Api.Tests.Unit.Helpers
{
    public static class UserStoryResponseHelper
    {
        public static UserStoryResponse ToUserStoryResponse(this UserStory userStory)
        {
            return new UserStoryResponse
            {
                Id = userStory.Id,
                Title = userStory.Title,
                Owner = userStory.Owner,
                Description = userStory.Description,
                AcceptanceCriteria = userStory.AcceptanceCriteria,
                DefinitionOfDone = userStory.DefinitionOfDone,
                DefintionOfReady = userStory.DefinitionOfReady,
                Comment = userStory.Comment,
                StoryPoints = userStory.StoryPoints,
                Priority = userStory.Priority,
                Risk = userStory.Risk,
                Deadline = userStory.Deadline,
                Created = userStory.Created
            };
        }
    }
}
