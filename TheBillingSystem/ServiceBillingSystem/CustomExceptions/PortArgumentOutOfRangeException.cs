using System;

namespace ServiceBillingSystem.CustomExceptions
{
    public sealed class PortArgumentOutOfRangeException : ArgumentOutOfRangeException
    {
        public int Id { get; }
        public override string Message { get; }


        public PortArgumentOutOfRangeException(int id)
        {
            Id = id;
            Message = string.Join(string.Empty, "Incorrect value: '", Id, "'. The value is in the range of existing ports.");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public PortArgumentOutOfRangeException(int initialEntry, int finalEntry)
        {
            Message = string.Join(string.Empty, "Incorrect value for range: '[", initialEntry, "; ", finalEntry, "]'. The value is in the range of existing ports.");
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(Message);
        }

        public PortArgumentOutOfRangeException(string message, int id) : base(message)
        {
            Id = id;
            Message = message;
            if (InnerException != null)
                ProgramLog.Exception(InnerException.Message);
            ProgramLog.Exception(message);
        }
    }
}
