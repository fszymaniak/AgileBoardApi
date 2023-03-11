using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 500)
            {
                throw new InvalidDescriptionException(value);
            }

            Value = value;
        }

        public static implicit operator string(Description description) => description.Value;

        public static implicit operator Description(string value) => new(value);
    }
}
