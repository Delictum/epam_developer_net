using System;
using System.Collections.Generic;
using TextProcessingForBook.CustomText.TextContent.Characters;

namespace TextProcessingForBook.CustomText.TextContent
{
    class AuxiliaryForSentence
    {
        public static List<Sentence> ToListSentence(string stringLine, ref List<Sentence> listSentence)
        {
            if (string.IsNullOrEmpty(stringLine))
                throw new ArgumentNullException("Empty string cannot be passed.");

            Character currentCharacter;
            var currentWord = new Word();
            var listPunctuationCharacater = new List<Character>();
            var currentSentence = new Sentence();

            Action clearTupleItems = () =>
            {
                currentWord.Value.Clear();
                listPunctuationCharacater.Clear();
            };

            for (int i = 0; i < stringLine.Length; i++)
            {
                currentCharacter = new Character(stringLine[i]);
                if (char.IsLetterOrDigit(currentCharacter.Value))
                {
                    if (listPunctuationCharacater.Count != 0)
                    {
                        currentSentence.AddLastWord(new Tuple<Word, List<Character>>(new Word(currentWord.Value), new List<Character>(listPunctuationCharacater)));
                        clearTupleItems();
                    }
                    currentWord.AddLastCharacter(currentCharacter);
                }
                else
                {
                    listPunctuationCharacater.Add(currentCharacter);

                    if (currentCharacter.Type == CharacterType.EndPunctuation)
                    {
                        currentSentence.AddLastWord(new Tuple<Word, List<Character>>(new Word(currentWord.Value), new List<Character>(listPunctuationCharacater)));
                        listSentence.Add(new Sentence(currentSentence.Value));
                        clearTupleItems();
                        currentSentence.Value.Clear();
                    }
                }
            }
            currentSentence.AddLastWord(new Tuple<Word, List<Character>>(new Word(currentWord.Value), new List<Character>(listPunctuationCharacater)));
            listSentence.Add(new Sentence(currentSentence.Value));
            clearTupleItems();
            return listSentence;
        }
    }
}
