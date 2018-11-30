using System;
using ContourBillingSystem;

namespace ModelBillingSystem
{
    public class CallLog : ICallLog
    {
        public ISubscriberNumber Called { get; }
        public DateTime StartedAt { get; }
        public TimeSpan Duration { get; private set; }
        public double Price { get; private set; }


        public CallLog(ISubscriberNumber called)
        {
            Called = called;
            StartedAt = DateTime.Now;
        }


        public void FinalInit(TimeSpan duration, double price)
        {
            Duration = duration;
            Price = price;
        }
    }
}
