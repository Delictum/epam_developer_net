using ContourBillingSystem.ComponentStation;
using System.Collections.Generic;

namespace ContourBillingSystem
{
    public interface IStation
    {
        IList<Port> Capacity { get; set; }
        CodecType Codec { get; set; }
    }
}