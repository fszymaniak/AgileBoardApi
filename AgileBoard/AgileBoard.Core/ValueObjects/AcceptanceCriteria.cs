using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record AcceptanceCriteria(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidAcceptanceCriteriaException();

        public static implicit operator string(AcceptanceCriteria acceptanceCriteria) => acceptanceCriteria.Value;

        public static implicit operator AcceptanceCriteria(string value) => new(value);
    }
}
