using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBillingSystem.CustomExceptions
{
    public class DisconnectTerminalException : Exception
    {
        public override string Message { get; }


        public DisconnectTerminalException()
        {
            Message = "The port is not used.";
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }
    }
}
