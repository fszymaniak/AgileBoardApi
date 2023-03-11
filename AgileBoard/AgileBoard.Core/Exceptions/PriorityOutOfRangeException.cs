namespace AgileBoard.Core.Exceptions
{
    public class PriorityOutOfRangeException : CustomException
    {
        public int? Value { get; }

        public PriorityOutOfRangeException(int? value) : base($"Priority: {value} out of range.")
        {
            Value = value;
        }
    }
}
