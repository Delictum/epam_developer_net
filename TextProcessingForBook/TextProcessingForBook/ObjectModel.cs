using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextProcessingForBook
{
    public static class ObjectModel
    {
        public static XDocument CreateObjectModel()
        {
            XDocument objectModel =
                new XDocument(
                    new XElement("TextProcessingForBook", new XAttribute("type", "namespace"),
                        new XElement("App", new XAttribute("type", "config")),
                        new XElement("Program", new XAttribute("type", "main")),
                        new XElement("ObjectModel", new XAttribute("type", "static class")),

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
            return objectModel;
        }

        public static void SaveObjectModel(XDocument objectModel, string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                filePath = ConfigurationManager.AppSettings["ObjectModel"];
            else
            {
                var builder = new StringBuilder();
                builder.Append(filePath).Append("\\").Append(fileName).Append(".xml");
                filePath = builder.ToString();
            }
            objectModel.Save(filePath);
        }
    }
}
