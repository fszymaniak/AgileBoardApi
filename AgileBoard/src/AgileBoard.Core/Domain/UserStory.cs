using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Domain
{
    public class UserStory
    {
        public UserStoryId Id { get; set; }

        public Title Title { get; set; } = null!;

        public Owner? Owner { get; set; }

        public Description? Description { get; set; }

        public AcceptanceCriteria? AcceptanceCriteria { get; set; }

        public DefinitionOfDone? DefinitionOfDone { get; set; }

        public DefinitionOfReady? DefinitionOfReady { get; set; }

        public Comment? Comment { get; set; }

        public StoryPoints? StoryPoints { get; set; }

        public Priority? Priority { get; set; }

        public Risk? Risk { get; set; }

        public Date? Deadline { get; set; }

        public Date Created { get; set; }

        public Date? Updated { get; set; }

        public UserStory(UserStoryId id,
            Title title,
            Owner? owner,
            Description? description,
            AcceptanceCriteria? acceptanceCriteria,
            DefinitionOfDone? definitionOfDone,
            DefinitionOfReady? defintionOfReady,
            Comment? comment,
            StoryPoints? storyPoints,
            Priority? priority,
            Risk? risk,
            Date? deadline,
            Date created,
            Date? updated)
        {
            Id = id;
            Title = title;
            Owner = owner;
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
            DefinitionOfDone = definitionOfDone;
            DefinitionOfReady = defintionOfReady;
            Comment = comment;
            StoryPoints = storyPoints;
            Priority = priority;
            Risk = risk;
            Deadline = deadline;
            Created = created;
            Updated = updated;
        }
    }
}
