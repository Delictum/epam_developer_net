using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBillingSystem.CustomExceptions
{
    public class GeneralizedException : Exception
    {
        public override string Message { get; }


        public GeneralizedException(string message) : base(message)
        {
            Message = message;
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }
    }
}
