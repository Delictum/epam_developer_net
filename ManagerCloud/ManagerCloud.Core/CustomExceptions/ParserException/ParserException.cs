using System;

namespace ManagerCloud.Core.CustomExceptions.ParserException
{
    public class ParserException : Exception
    {
        public string FileName { get; }
        public int ErrorString { get; }
        public override string Message { get; }

        public ParserException(string fileName, int errorString)
        {
            ErrorString = errorString;
            FileName = fileName;
            Message = string.Join(string.Empty, base.Message, "The exception in the file \"", fileName, "\" on line ", errorString);
        }

        public ParserException(string fileName, int currentLine, string message)
        {
            FileName = fileName;
            Message = message;
        }
    }
}
