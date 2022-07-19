using AgileBoard.Api.Clock;
using AgileBoard.Api.Contracts.Requests;
using AgileBoard.Api.Domain;

namespace AgileBoard.Api.Extensions
{
    public static class UserStoryExtensions
    {
        private static readonly IClock? _clock;

        public static void UpdateUserStory(this UserStory userStory, UpdateUserStoryRequest request)
        {
            userStory.Title = request.Title;
            userStory.Owner = request.Owner;
            userStory.Description = request.Description;
            userStory.AcceptanceCriteria = request.AcceptanceCriteria;
            userStory.DefinitionOfDone = request.DefinitionOfDone;
            userStory.DefintionOfReady = request.DefintionOfReady;
            userStory.Comments = request.Comments;
            userStory.StoryPoints = request.StoryPoints;
            userStory.Priority = request.Priority;
            userStory.Risk = request.Risk;
            userStory.Deadline = request.Deadline;
            userStory.Updated = _clock.DateTimeNow;
        }
    }
}
