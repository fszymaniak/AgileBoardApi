namespace AgileBoard.Core.Validators
{
    public sealed class FibonacciValidator
    {
        public static bool IsFibonacci(int n)
        {
            double x1 = 5 * Math.Pow(n, 2) + 4;
            double x2 = 5 * Math.Pow(n, 2) - 4;

            long x1_sqrt = (long)Math.Sqrt(x1);
            long x2_sqrt = (long)Math.Sqrt(x2);

            return x1_sqrt * x1_sqrt == x1 || x2_sqrt * x2_sqrt == x2;
        }
    }
}
