namespace AgileBoard.Core.Exceptions
{
    public class StoryPointNotFibonacciNumber : CustomException
    {
        public int Number { get; }

        public StoryPointNotFibonacciNumber(int number) : base($"Story Point value {number} is not a valid Fibonacci number.")
        {
            Number = number;
        }
    }
}
