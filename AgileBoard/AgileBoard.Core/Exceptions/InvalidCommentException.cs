namespace AgileBoard.Core.Exceptions
{
    public class InvalidCommentException : CustomException
    {
        public string Comment { get; }

        public InvalidCommentException(string comment) : base($"Invalid comment: {comment}")
        {
            Comment = comment;
        }
    }
}
