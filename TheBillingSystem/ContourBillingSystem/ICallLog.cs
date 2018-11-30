using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourBillingSystem
{
    public interface ICallLog
    {
        ISubscriberNumber Called { get; }
        DateTime StartedAt { get; }
        TimeSpan Duration { get; }
        double Price { get; }

        void FinalInit(TimeSpan duration, double price);
    }
}
