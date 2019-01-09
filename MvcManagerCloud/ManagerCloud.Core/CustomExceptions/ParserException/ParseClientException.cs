namespace ManagerCloud.Core.CustomExceptions.ParserException
{
    public class ParseClientException : ParserException
    {
        public string[] ValueClient { get; }
        public override string Message { get; }

        public ParseClientException(string[] valueClient, int clientPropertiesCount, string fileName, int errorString) : base(fileName, errorString)
        {
            ValueClient = valueClient;
            Message = string.Join(string.Empty, base.Message, "Invalid value client: \"", string.Join(" ", valueClient),
                "\". Requires ", clientPropertiesCount, " words, separated by a white space.");
        }

        public ParseClientException(string[] valueClient, string fileName, int errorString, string message) : base(fileName, errorString, message)
        {
            ValueClient = valueClient;
            Message = message;
        }
    }
}
