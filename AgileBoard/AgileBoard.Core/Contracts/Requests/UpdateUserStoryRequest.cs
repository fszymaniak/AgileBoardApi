using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Contracts.Requests
{
    public class UpdateUserStoryRequest
    {
        public Title Title { get; set; } = null!;

        public Owner? Owner { get; set; }

        public Description? Description { get; set; }

        public AcceptanceCriteria? AcceptanceCriteria { get; set; }

        public DefinitionOfDone? DefinitionOfDone { get; set; }

        public DefinitionOfReady? DefintionOfReady { get; set; }

        public Comment? Comment { get; set; }

        public StoryPoints? StoryPoints { get; set; }

        public Priority? Priority { get; set; }

        public Risk? Risk { get; set; }

        public Date? Deadline { get; set; }

        public Date? Updated { get; set; }
    }
}
