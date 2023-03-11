using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Validators;

namespace AgileBoard.Core.ValueObjects
{
    public sealed record StoryPoints
    {
        public int Value { get; }

        public StoryPoints(int value)
        {
            if (FibonacciValidator.IsFibonacci(value) is false)
            {
                throw new StoryPointNotFibonacciNumber(value);
            }

            Value = value;
        }

        public static implicit operator int(StoryPoints storyPoints) => storyPoints.Value;

        public static implicit operator StoryPoints(int value) => new(value);
    }
}
