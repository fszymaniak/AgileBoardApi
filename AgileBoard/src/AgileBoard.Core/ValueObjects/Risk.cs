using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Risk
    {
        public static IEnumerable<string> AvailableRisks { get; } = new[] { "Low", "Medium", "High" };

        public string Value { get; }

        public Risk(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidRiskException(value);
            }

            if (AvailableRisks.Contains(value) is false)
            {
                throw new InvalidRiskException(value);
            }

            Value = value;
        }

        public static Risk Low() => new("Low");

        public static Risk Medium() => new("Medium");

        public static Risk High() => new("High");

        public static implicit operator Risk(string value) => new(value);

        public static implicit operator string(Risk value) => value.Value;
    }
}
