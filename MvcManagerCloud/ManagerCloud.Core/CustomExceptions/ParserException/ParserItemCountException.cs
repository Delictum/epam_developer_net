namespace ManagerCloud.Core.CustomExceptions.ParserException
{
    public class ParserItemCountException : ParserException
    {
        public int IncorrectItemCount { get; }
        public override string Message { get; }

        public ParserItemCountException(int incorrectValueCount, int correctValueCount, string fileName, int errorString) : base(fileName, errorString)
        {
            IncorrectItemCount = incorrectValueCount;
            Message = string.Join(string.Empty, base.Message, "Invalid value item count: \"", IncorrectItemCount,
                "\". Requires \"", correctValueCount, "\" items separated by a comma.");
        }

        public ParserItemCountException(int incorrectValueCount, string fileName, int currentLine, string message) : base(fileName, currentLine, message)
        {
            IncorrectItemCount = incorrectValueCount;
            Message = message;
        }
    }
}
