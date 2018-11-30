using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBillingSystem.CustomExceptions
{
    public class ConnectTerminalException : Exception
    {
        public override string Message { get; }


        public ConnectTerminalException()
        {
            Message = "The port is already in use.";
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }
    }
}
