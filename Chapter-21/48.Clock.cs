using System;

namespace HelloIoC
{
    public class Clock : IClock
    {
        public DateTime TimeNow
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
    
    public interface IClock
    {
        DateTime TimeNow { get; }
    }
}
