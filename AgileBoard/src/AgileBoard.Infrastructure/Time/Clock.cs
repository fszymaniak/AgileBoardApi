using AgileBoard.Core.Abstractions;

namespace AgileBoard.Infrastructure.Time
{
    public class Clock : IClock
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}
