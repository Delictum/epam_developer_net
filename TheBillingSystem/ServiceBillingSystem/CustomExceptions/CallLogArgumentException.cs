using System;

namespace ServiceBillingSystem.CustomExceptions
{
    public class CallLogArgumentException : ArgumentException
    {
        public override string Message { get; }


        public CallLogArgumentException(int value)
        {
            Message = string.Join(string.Empty, "Incorrect value: '", value, "'. The value should be no more than 30.");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public CallLogArgumentException(double value)
        {
            Message = string.Join(string.Empty, "Incorrect value: '", value, "'. Cost can't be less than zero.");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public CallLogArgumentException(DateTime value)
        {
            Message = string.Join(string.Empty, "Incorrect value: '", value, "'. Date can't be longer than the current time.");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public CallLogArgumentException(string message) : base(message)
        {
            Message = message;
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(message);
        }
    }
}
