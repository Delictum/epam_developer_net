using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBillingSystem.CustomExceptions
{
    public class FreePortNotFoundException : KeyNotFoundException
    {
        public override string Message { get; }


        public FreePortNotFoundException()
        {
            Message = "No ports available.";
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }
    }
}
