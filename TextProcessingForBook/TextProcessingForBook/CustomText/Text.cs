using System;
using System.Collections.Generic;
using System.Text;
using TextProcessingForBook.CustomText.TextContent;
using TextProcessingForBook.CustomText.TextContent.Characters;
using System.IO;

namespace TextProcessingForBook.CustomText
{
    public class Text : IText
    {
        public List<Sentence> Value { get; private set; }


        public Text()
        {
            Value = new List<Sentence>();
        }

        public Text (StreamReader setText)
        {
            CreateNewText(setText);
        }

        public Text(List<Sentence> setText)
        {
            Value = setText;
        }


        private List<Sentence> ParseFileLoad(StreamReader text)
        {
            var listPunctuationCharacater = new List<Character>();
            var listSentence = new List<Sentence>();

            while (!text.EndOfStream)
            {
                string stringLine = text.ReadLine();
                if (string.IsNullOrEmpty(stringLine))
                    continue;
                
                stringLine = TextProcessing.NormalizeWhiteSpace(stringLine);
                listSentence = AuxiliaryForSentence.ToListSentence(stringLine, ref listSentence);
                listSentence[listSentence.Count-1].Value[listSentence[listSentence.Count-1].Value.Count-1].Item2.Add(new Character((char)10));
            }
            return listSentence;
        }

        public void CreateNewText(StreamReader text)
        {
            try
            {
                Value = ParseFileLoad(text);
            }
            catch(EndOfStreamException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Display()
        {
            if (Value == null)
                throw new ArgumentNullException("Not load text.");
            Console.WriteLine(ToString());
        }

        public void AddLastSentence(Sentence sentenceToAdd)
        {
            Value.Add(sentenceToAdd);
        }

        public void AddSentence(Sentence sentenceToAdd, int index = 0)
        {
            Value.Insert(index, sentenceToAdd);
        }

        public void ReplaceSentence(Sentence oldSentence, Sentence newSentence)
        {
            if (Value.Exists(sentence => sentence.Value == oldSentence.Value))
            {
                int index = Value.FindIndex(sentence => sentence.Value == oldSentence.Value);
                Value[index].ReplaceSentence(newSentence);
            }
            else
                throw new ArgumentException("Replaceable sentence does not exist.");
        }

        public void DeleteSentenceByPosition(int index)
        {
            if (Value.Count > index && index > -1)
                Value.RemoveAt(index);
            else
                throw new IndexOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
        }


        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach (var sentence in Value)
            {
                foreach (var tupleWord in sentence.Value)
                {
                    foreach (var letterOrDigitCharacter in tupleWord.Item1.Value)
                    {
                        text.Append(letterOrDigitCharacter.Value);
                    }
                    foreach (var character in tupleWord.Item2)
                    {
                        text.Append(character.Value);
                    }
                }
            }
            return text.ToString();
        }

        public override bool Equals(object obj)
        {
            var text = obj as Text;
            return text != null &&
                   EqualityComparer<List<Sentence>>.Default.Equals(Value, text.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<List<Sentence>>.Default.GetHashCode(Value);
        }
    }
}
