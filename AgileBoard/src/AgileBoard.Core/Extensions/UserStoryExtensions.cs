using AgileBoard.Core.Contracts.Requests;
using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Extensions
{
    public static class UserStoryExtensions
    {
        public static void UpdateUserStory(this UserStory userStory, UpdateUserStoryRequest request)
        {
            userStory.Title = request.Title;
            userStory.Owner = request.Owner;
            userStory.Description = request.Description;
            userStory.AcceptanceCriteria = request.AcceptanceCriteria;
            userStory.DefinitionOfDone = request.DefinitionOfDone;
            userStory.DefinitionOfReady = request.DefintionOfReady;
            userStory.Comment = request.Comment;
            userStory.StoryPoints = request.StoryPoints;
            userStory.Priority = request.Priority;
            userStory.Risk = request.Risk;
            userStory.Deadline = request.Deadline;
            userStory.Updated = Date.Now;
        }
    }
}
