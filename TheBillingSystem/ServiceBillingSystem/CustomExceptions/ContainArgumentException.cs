using System;

namespace ServiceBillingSystem.CustomExceptions
{
    public sealed class ContainArgumentException : ArgumentException
    {
        public string Value { get; }
        public override string Message { get; }


        public ContainArgumentException(string value)
        {
            Value = value;
            Message = string.Join(string.Empty, "Item - ", Value, " not found in list");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public ContainArgumentException(string message, string value) : base(message)
        {
            Value = value;
            Message = message;
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }
    }
}
