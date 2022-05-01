namespace AgileBoard.Api.Contracts.Requests
{
    public class CreateUserStoryRequest
    {
        public string Title { get; set; }

        public string? Owner { get; set; }

        public string? Description { get; set; }

        public string? AcceptanceCriteria { get; set; }

        public string? DefinitionOfDone { get; set; }

        public string? DefintionOfReady { get; set; }

        public string? Comments { get; set; }

        public int? StoryPoints { get; set; }

        public int? Priority { get; set; }

        public string? Risk { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
