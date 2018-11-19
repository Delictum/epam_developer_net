using System;
using System.IO;
using System.Configuration;
using TextProcessingForBook.CustomText;
using TextProcessingForBook.CustomText.TextContent;
using TextProcessingForBook.CustomText.TextContent.Characters;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TextProcessingForBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //Text zenText;
            //var filePath = ConfigurationManager.AppSettings["FilePath"];
            //using (var textFile = File.OpenText(filePath))
            //{
            //    try
            //    {
            //        zenText = new Text(textFile);
            //    }
            //    finally
            //    {
            //        if (textFile != null)
            //            ((IDisposable)textFile).Dispose();
            //    }
            //}
            //zenText.Display();
            //Console.ReadKey();
            //Console.Clear();

            //var newText1 = TextProcessing.OrderByAscendingCountWordsInSentence(zenText);
            //newText1.Display();
            //Console.ReadKey();
            //Console.Clear();

            //int lengthWord = 3;
            //var listWords = TextProcessing.FindAllWordByLength(
            //        TextProcessing.FindAllSentenceType(zenText, SentenceType.Interrogative),
            //        lengthWord,
            //        true);
            //Console.WriteLine(string.Join(", ", listWords));
            //Console.ReadKey();
            //Console.Clear();

            //int index = 0;
            //var newText2 = TextProcessing.DeleteWordsContainingCharacterType(
            //        zenText,
            //        CharacterType.Consonant,
            //        index,
            //        TextProcessing.FindAllWordByLength(zenText, lengthWord));
            //newText2.Display();
            //Console.ReadKey();
            //Console.Clear();

            //var newText3 = TextProcessing.ReplaceWord(zenText, TextProcessing.FindAllWordByLength(zenText, lengthWord), "ERROR");
            //newText3.Display();

            CreateObjectModel();
        }

        public static void CreateObjectModel()
        {
            XDocument xDocument =
                new XDocument(
                    new XElement("TextProcessingForBook", new XAttribute("type", "namespace"),
                        new XElement("App", new XAttribute("type", "config")),
                        new XElement("Program", new XAttribute("type", "main")),                        

                        new XElement("CustomText", new XAttribute("type", "folder"),
                            new XElement("IText", new XAttribute("type", "interface")),
                            new XElement("Text", new XAttribute("type", "class")),
                            new XElement("TextProcessing", new XAttribute("type", "class")),

                            new XElement("TextContent", new XAttribute("type", "folder"),
                                new XElement("AuxiliaryForSentence", new XAttribute("type", "static class")),
                                new XElement("Sentence", new XAttribute("type", "class")),
                                new XElement("SentenceType", new XAttribute("type", "enum")),
                                new XElement("Word", new XAttribute("type", "class"))),

                                new XElement("Characters", new XAttribute("type", "folder"),
                                    new XElement("Character", new XAttribute("type", "class")),                                    
                                    new XElement("CharacterHelper", new XAttribute("type", "static class")),
                                    new XElement("CharacterType", new XAttribute("type", "enum"))))));

            var filePath = ConfigurationManager.AppSettings["ObjectModel"];
            xDocument.Save(filePath);
            Console.WriteLine(xDocument.ToString());
        }
    }
}
