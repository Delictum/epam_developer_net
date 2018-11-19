using System.IO;


namespace TextProcessingForBook.CustomText.TextContent
{
    public interface IText
    {
        void CreateNewText(StreamReader text);

        void Display();

        void AddLastSentence(Sentence sentenceToAdd);

        void AddSentence(Sentence sentenceToAdd, int index = 0);

        void DeleteSentenceByPosition(int index);
    }
}
