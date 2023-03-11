using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record DefinitionOfReady(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidDefinitionOfReadyException();

        public static implicit operator string(DefinitionOfReady title) => title.Value;

        public static implicit operator DefinitionOfReady(string value) => new(value);
    }
}
