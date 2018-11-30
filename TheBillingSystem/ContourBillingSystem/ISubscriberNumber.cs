using System.Collections.Generic;

namespace ContourBillingSystem
{
    public interface ISubscriberNumber
    {
        IRate Rate { get; }
        int Number { get; }
        IList<ICallLog> CallLog { get; }
    }
}