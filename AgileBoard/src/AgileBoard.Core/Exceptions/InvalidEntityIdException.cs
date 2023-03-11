namespace AgileBoard.Core.Exceptions
{
    public class InvalidEntityIdException : CustomException
    {
        public object Id { get; }

        public InvalidEntityIdException(object id) : base($"Unable to set: {id} as entity identifier.")
            => Id = id;
    }
}
