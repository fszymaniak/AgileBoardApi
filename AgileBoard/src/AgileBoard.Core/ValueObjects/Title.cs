using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Title(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidUserStoryTitleException();

        public static implicit operator string(Title title) => title.Value;

        public static implicit operator Title(string value) => new(value);
    }
}
