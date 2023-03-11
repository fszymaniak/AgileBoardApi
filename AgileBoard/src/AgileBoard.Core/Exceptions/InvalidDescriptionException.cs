namespace AgileBoard.Core.Exceptions
{
    public class InvalidDescriptionException : CustomException
    {
        public string Description { get; }

        public InvalidDescriptionException(string description) : base($"Invalid description: {description}")
        {
            Description = description;
        }
    }
}
