using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record Priority
    {
        public int? Value { get; set; }

        public Priority(int? value)
        {
            if (value == null || value is <= 0 or > 4)
            {
                throw new PriorityOutOfRangeException(value);
            }

            Value = value;
        }

        public static implicit operator int?(Priority priority) => priority.Value;

        public static implicit operator Priority(int? value) => new(value);
    }
}
