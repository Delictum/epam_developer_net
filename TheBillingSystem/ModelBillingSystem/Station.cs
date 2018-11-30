using ContourBillingSystem;
using System.Collections.Generic;
using ContourBillingSystem.ComponentStation;

namespace ModelBillingSystem
{
    public class Station : IStation
    {
        public IList<Port> Capacity { get; set; }
        public CodecType Codec { get; set; }


        public Station(IList<Port> capacity, CodecType codec)
        {
            Capacity = capacity;
            Codec = codec;
        }
    }
}
