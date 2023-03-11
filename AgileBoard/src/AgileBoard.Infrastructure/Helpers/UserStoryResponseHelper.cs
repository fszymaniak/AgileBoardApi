using AgileBoard.Core.Contracts.Responses;
using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.Helpers
{
    public static class UserStoryResponseHelper
    {
        public static UserStoryResponse ToUserStoryResponse(this UserStory userStory)
        {
            return new UserStoryResponse
            {
                Id = userStory.Id.Value,
                Title = new Title(userStory.Title.Value),
                Owner = new Owner(userStory.Owner.Value),
                Description = userStory.Description.Value,
                AcceptanceCriteria = new AcceptanceCriteria(userStory.AcceptanceCriteria.Value),
                DefinitionOfDone = new DefinitionOfDone(userStory.DefinitionOfDone.Value),
                DefintionOfReady = new DefinitionOfReady(userStory.DefinitionOfReady.Value),
                Comment = userStory.Comment,
                StoryPoints = userStory.StoryPoints.Value,
                Priority = userStory.Priority.Value,
                Risk = userStory.Risk.Value,
                Deadline = userStory.Deadline.Value,
                Created = userStory.Created.Value
            };
        }
    }
}   
