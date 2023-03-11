namespace AgileBoard.Core.Exceptions
{
    public class InvalidRiskException : CustomException
    {
        public string Risk { get; }
        public InvalidRiskException(string risk) : base($"Risk value: '{risk}' is invalid.")
        {
            Risk = risk;
        }
    }
}
