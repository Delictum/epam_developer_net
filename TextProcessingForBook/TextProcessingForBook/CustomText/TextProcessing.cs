using System.Collections.Generic;
using System.Linq;
using TextProcessingForBook.CustomText.TextContent;
using TextProcessingForBook.CustomText.TextContent.Characters;

namespace TextProcessingForBook.CustomText
{
    public static class TextProcessing
    {
        public static Text OrderByAscendingCountWordsInSentence(Text text)
        {
            List<Sentence> sortedList = text.Value.OrderBy(o => o.Value.Count).ToList();
            Text newText = new Text(sortedList);
            return newText;
        }

        public static Text FindAllSentenceType(Text text, SentenceType type)
        {
            Text resultSentences = new Text();
            foreach (var sentence in text.Value)
            {
                if (sentence.Type == type)
                    resultSentences.Value.Add(sentence);
            }
            return resultSentences;
        }

        public static List<Word> FindAllWordByLength(Text text, int lengthWord, bool distinct = false)
        {
            List<Word> resultListWords = new List<Word>();  
            foreach (var sentence in text.Value)
            {
                foreach(var tuple in sentence.Value)
                {
                    if (tuple.Item1.Value.Count == lengthWord)
                    {
                        if (distinct && !ExistsWordInListWords(resultListWords, tuple.Item1))
                            resultListWords.Add(tuple.Item1);
                        else if (!distinct)
                            resultListWords.Add(tuple.Item1);
                    }
                }
            }
            return resultListWords.ToList();
        }

        public static bool ExistsWordInListWords(List<Word> words, Word word)
        {
            return words.Exists(x => x.Equals(word));
        }

        public static List<Sentence> FindAllSentencesContainingWordByLength(Text text, int lengthWord)
        {
            List<Sentence> resulListSenetece = new List<Sentence>();
            foreach (var sentence in text.Value)
            {
                foreach (var tuple in sentence.Value)
                {
                    if (tuple.Item1.Value.Count == lengthWord)
                    {
                        resulListSenetece.Add(sentence);
                        break;
                    }
                }
            }
            return resulListSenetece;
        }

        public static Text DeleteWordsContainingCharacterType(Text text, CharacterType characterType, int index = -1, List<Word> listWords = null)
        {
            foreach (var sentence in text.Value)
            {
                foreach (var tuple in sentence.Value)
                {
                    if (listWords != null)
                    {
                        if (!ExistsWordInListWords(listWords, tuple.Item1))                            
                            continue;
                    }
                    if (index != -1)
                    {
                        if (tuple.Item1.Value.Count > index)
                            if (tuple.Item1.Value[index].Type == characterType)
                                tuple.Item1.Value.Clear();
                    }
                    else
                    {
                        foreach (var character in tuple.Item1.Value)
                        {
                            if (character.Type == characterType)
                            {
                                tuple.Item1.Value.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            return text;
        }

        public static Text ReplaceWord(Text text, List<Word> listOldWords, string newStr)
        {
            Word newWord = new Word(newStr);
            foreach (var sentence in text.Value)
            {
                foreach (var tuple in sentence.Value)
                {
                    if (!ExistsWordInListWords(listOldWords, tuple.Item1))
                        continue;
                    tuple.Item1.ReplaceWord(newWord);
                }
            }
            return text;
        }

        public static string NormalizeWhiteSpace(string input, char normalizeTo = ' ')
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            int current = 0;
            char[] output = new char[input.Length];
            bool skipped = false;

            foreach (char c in input.ToCharArray())
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!skipped)
                    {
                        if (current > 0)
                            output[current++] = normalizeTo;

                        skipped = true;
                    }
                }
                else
                {
                    skipped = false;
                    output[current++] = c;
                }
            }
            if (current == 0)
                return string.Empty;

            return new string(output, 0, skipped ? current - 1 : current);
        }
    }
}
