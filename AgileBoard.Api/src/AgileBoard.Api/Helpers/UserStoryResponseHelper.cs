﻿using AgileBoard.Api.Contracts.Responses;
using AgileBoard.Api.Domain;

namespace AgileBoard.Api.Helpers
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
                DefintionOfReady = userStory.DefintionOfReady,
                Comments = userStory.Comments,
                StoryPoints = userStory.StoryPoints,
                Priority = userStory.Priority,
                Risk = userStory.Risk,
                Deadline = userStory.Deadline,
                Created = userStory.Created
            };
        }
    }
}
