namespace AgileBoard.Api.Clock
{
    public class Clock : IClock
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}
