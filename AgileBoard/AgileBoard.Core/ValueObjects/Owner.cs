using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Owner
    {
        public string Value { get; }

        public Owner(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidUserStoryOwnerException(value);
            }
            if (value.Length is > 100 && value.Length is < 3)
            {
                throw new InvalidUserStoryOwnerException(value);
            }

            Value = value;
        }

        public static implicit operator string(Owner owner) => owner.Value;

        public static implicit operator Owner(string name) => new(name);

    }
}
