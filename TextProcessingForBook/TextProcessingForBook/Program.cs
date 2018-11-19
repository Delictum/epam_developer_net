using System;
using System.IO;
using System.Configuration;
using TextProcessingForBook.CustomText;
using TextProcessingForBook.CustomText.TextContent;
using TextProcessingForBook.CustomText.TextContent.Characters;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

namespace TextProcessingForBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Text zenText;
            var filePath = ConfigurationManager.AppSettings["FilePath"];
            using (var textFile = File.OpenText(filePath))
            {
                try
                {
                    zenText = new Text(textFile);
                }
                finally
                {
                    if (textFile != null)
                        ((IDisposable)textFile).Dispose();
                }
            }
            zenText.Display();
            Console.ReadKey();
            Console.Clear();

            var newText1 = TextProcessing.OrderByAscendingCountWordsInSentence(zenText);
            newText1.Display();
            Console.ReadKey();
            Console.Clear();

            int lengthWord = 3;
            var listWords = TextProcessing.FindAllWordByLength(
                    TextProcessing.FindAllSentenceType(zenText, SentenceType.Interrogative),
                    lengthWord,
                    true);
            Console.WriteLine(string.Join(", ", listWords));
            Console.ReadKey();
            Console.Clear();

            int index = 0;
            var newText2 = TextProcessing.DeleteWordsContainingCharacterType(
                    zenText,
                    CharacterType.Consonant,
                    index,
                    TextProcessing.FindAllWordByLength(zenText, lengthWord));
            newText2.Display();
            Console.ReadKey();
            Console.Clear();

            var newText3 = TextProcessing.ReplaceWord(zenText, TextProcessing.FindAllWordByLength(zenText, lengthWord), "ERROR");
            newText3.Display();
            Console.ReadKey();
            Console.Clear();

            var objectModel = ObjectModel.CreateObjectModel();
            //ObjectModel.SaveObjectModel(objectModel, "ObjectModel", @"C:\Users\thedr\Desktop");
            Console.WriteLine(objectModel);
        }        
    }
}
