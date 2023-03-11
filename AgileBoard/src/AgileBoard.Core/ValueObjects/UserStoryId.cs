using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record UserStoryId
    {
        public Guid Value { get; }

        public UserStoryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidEntityIdException(value);
            }

            Value = value;
        }

        public static UserStoryId Create() => new(Guid.NewGuid());

        public static implicit operator Guid(UserStoryId userStoryId) => userStoryId.Value;

        public static implicit operator UserStoryId(Guid value) => new(value);
    }
}
