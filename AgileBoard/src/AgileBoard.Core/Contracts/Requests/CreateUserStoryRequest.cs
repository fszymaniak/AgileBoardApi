using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Contracts.Requests
{
    public class CreateUserStoryRequest
    {
        public Title Title { get; set; } = null!;

        public Owner? Owner { get; set; } = null!;

        public Description? Description { get; set; } = null!;

        public AcceptanceCriteria? AcceptanceCriteria { get; set; } = null!;

        public DefinitionOfDone? DefinitionOfDone { get; set; } = null!;

        public DefinitionOfReady? DefinitionOfReady { get; set; } = null!;

        public Comment? Comment { get; set; }

        public StoryPoints? StoryPoints { get; set; } = null!;

        public Priority? Priority { get; set; } = null!;

        public Risk? Risk { get; set; } = null!;

        public Date? Deadline { get; set; } = null!;
    }
}
