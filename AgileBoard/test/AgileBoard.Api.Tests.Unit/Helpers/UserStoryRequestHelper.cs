using AgileBoard.Core.Contracts.Requests;
using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;
using System;

namespace AgileBoard.Api.Tests.Unit.Helpers
{
    public static class UserStoryRequestHelper
    {
        public static CreateUserStoryRequest ToCreateUserStoryRequest(this UserStory userStory)
        {
            return new CreateUserStoryRequest
            {
                Title = userStory.Title,
                Owner = userStory.Owner,
                Description = userStory.Description,
                AcceptanceCriteria = userStory.AcceptanceCriteria,
                DefinitionOfDone = userStory.DefinitionOfDone,
                DefinitionOfReady = userStory.DefinitionOfReady,
                Comment = userStory.Comment,
                StoryPoints = userStory.StoryPoints,
                Priority = userStory.Priority,
                Risk = userStory.Risk,
                Deadline = userStory.Deadline,
            };
        }

        public static UpdateUserStoryRequest ToUpdateUserStoryRequest(this UserStory userStory)
        {
            return new UpdateUserStoryRequest
            {
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
                Updated = Date.Now
            };
        }
    }
}
