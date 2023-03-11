namespace AgileBoard.Core.Exceptions
{
    public class InvalidDefinitionOfDoneException : CustomException
    {
        public InvalidDefinitionOfDoneException() : base("Invalid Definition of Done.")
        {
        }
    }
}
