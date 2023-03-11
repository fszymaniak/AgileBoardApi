namespace AgileBoard.Core.Exceptions
{
    public class InvalidUserStoryOwnerException : CustomException
    {
        public string OwnerName { get; }
        public InvalidUserStoryOwnerException(string ownerName) : base($"Invalid user story owner name: {ownerName}")
        {
            OwnerName = ownerName;
        }
    }
}
