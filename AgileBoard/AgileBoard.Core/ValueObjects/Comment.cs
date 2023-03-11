using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Comment
    {
        public string Value { get; }

        public Comment(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 500)
            {
                throw new InvalidCommentException(value);
            }

            Value = value;
        }

        public static implicit operator string(Comment description) => description.Value;

        public static implicit operator Comment(string value) => new(value);
    }
}
