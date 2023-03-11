using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record DefinitionOfDone(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidDefinitionOfDoneException();

        public static implicit operator string(DefinitionOfDone title) => title.Value;

        public static implicit operator DefinitionOfDone(string value) => new(value);
    }
}
