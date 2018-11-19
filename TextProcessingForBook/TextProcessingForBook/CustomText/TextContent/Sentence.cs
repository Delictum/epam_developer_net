using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TextProcessingForBook.CustomText.TextContent.Characters;

namespace TextProcessingForBook.CustomText.TextContent
{
    public class Sentence : IComparable, IComparer<Sentence>, IEquatable<Sentence>
    {
        public List<Tuple<Word, List<Character>>> Value { get; private set; }
        public SentenceType Type { get; private set; }
        ReadOnlyCollection<char> elementsSentenceType = new ReadOnlyCollection<char>(new List<char>() { '?', '!', '.' });


        public Sentence()
        {
            Value = new List<Tuple<Word, List<Character>>>();
        }

        public Sentence(List<Tuple<Word, List<Character>>> words)
        {
            Value = new List<Tuple<Word, List<Character>>>(words);
            SetTypeInListTupleWords(words);
        }

                
        private void SetType(List<Character> characters)
        {
            var ch = characters.Find(character => elementsSentenceType.Contains(character.Value));
            switch (ch.Value)
            {
                case '.':
                    Type = SentenceType.Declarative;
                    break;
                case '!':
                    Type = SentenceType.Exclamatory;
                    break;
                case '?':
                    Type = SentenceType.Interrogative;
                    break;
            }
        }

        private void SetTypeInListTupleWords(List<Tuple<Word, List<Character>>> tupleWords)
        {
            foreach (var tuple in tupleWords)
            {
                if (tuple.Item2.Exists(character => elementsSentenceType.Contains(character.Value)))
                {
                    SetType(tuple.Item2);
                }
            }
        }

        public Sentence ReplaceSentence(Sentence sentence)
        {
            Value = sentence.Value;
            return this;
        }

        public void AddLastWord(Tuple<Word, List<Character>> wordToAdd)
        {
            Value.Add(wordToAdd);
            if (wordToAdd.Item2.Exists(character => elementsSentenceType.Contains(character.Value)))
            {
                SetType(wordToAdd.Item2);
            }
        }

        public void AddWord(Tuple<Word, List<Character>> wordToAdd, int index = 0)
        {
            Value.Insert(index, wordToAdd);
        }

        public void ReplaceWord(Word oldWord, Word newWord)
        {
            if (Value.Exists(word => word.Item1.Value == oldWord.Value))
            {
                int index = Value.FindIndex(word => word.Item1.Value == oldWord.Value);
                Value[index].Item1.ReplaceWord(newWord);
            }
            else
                throw new ArgumentException("Replaceable word does not exist.");
        }

        public void DeleteWordByPosition(int index)
        {
            if (Value.Count > index && index > -1)
                Value.RemoveAt(index);
            else
                throw new IndexOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
        }


        public int CompareTo(object obj)
        {
            if (obj is Sentence)
                return Value.Count.CompareTo(((Sentence)obj).Value.Count);
            return -1;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, Value);
        }

        public override bool Equals(object obj)
        {
            var sentence = obj as Sentence;
            return sentence != null &&
                   EqualityComparer<List<Tuple<Word, List<Character>>>>.Default.Equals(Value, sentence.Value);
        }

        public override int GetHashCode()
        {
            return -1037169414 + EqualityComparer<List<Tuple<Word, List<Character>>>>.Default.GetHashCode(Value);
        }

        public int Compare(Sentence x, Sentence y)
        {
            if (x.Value.Count == y.Value.Count)
                return 0;
            else if (x.Value.Count < y.Value.Count)
                return -1;
            else
                return 1;
        }

        public bool Equals(Sentence other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Join(string.Empty, Value) == string.Join(string.Empty, other.Value);
        }
    }
}
