using System.Collections.Generic;
using ContourBillingSystem;

namespace ModelBillingSystem
{
    public class SubscriberNumber : ISubscriberNumber
    {
        public IRate Rate { get; }
        public int Number { get; }
        public IList<ICallLog> CallLog { get; }


        public SubscriberNumber(IRate rate, int numbers)
        {
            Rate = rate;
            Number = numbers;
            CallLog = new List<ICallLog>();
        }
    }
}
