namespace AgileBoard.Core.ValueObjects
{
    public sealed record Date
    {
        public DateTimeOffset Value { get; }

        public Date(DateTimeOffset value)
        {
            Value = value.Date;
        }

        public Date AddDays(int days) => new(Value.AddDays(days));

        public static implicit operator DateTimeOffset?(Date? date)
            => date!.Value;

        public static implicit operator Date?(DateTimeOffset? value)
            => new(value!);

        public static Date Now => new(DateTimeOffset.Now);

        public static Date MinValue => new(DateTimeOffset.MinValue);
    }
}
