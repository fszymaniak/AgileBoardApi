namespace AgileBoard.Core.Exceptions
{
    public class InvalidUserStoryTitleException : CustomException
    {
        public InvalidUserStoryTitleException() : base("User Story Title is invalid.")
        {
        }
    }
}
