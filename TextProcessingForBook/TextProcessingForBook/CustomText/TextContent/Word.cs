using System;
using System.Collections.Generic;
using TextProcessingForBook.CustomText.TextContent.Characters;

namespace TextProcessingForBook.CustomText.TextContent
{
    public class Word : IComparable, IComparer<Word>, IEquatable<Word>
    {
        public List<Character> Value { get; private set; }


        public Word()
        {
            Value = new List<Character>();
        }

        public Word(List<Character> characters)
        {
            Value = new List<Character>(characters);
        }

        public Word(string str)
        {
            Value = ToListCharacters(str);
        }


        private List<Character> ToListCharacters(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("Empty string cannot be passed.");
            var newList = new List<Character>();
            for (int i = 0; i < str.Length; i++)
            {
                newList.Add(new Character(str[i]));
            }
            return newList;
        }

        public Word ReplaceWord(Word word)
        {
            Value = word.Value;
            return this;
        }

        public void AddLastCharacter(Character characterToAdd)
        {
            Value.Add(characterToAdd);
        }

        public void AddCharacter(Character characterToAdd, int index = 0)
        {
            Value.Insert(index, characterToAdd);
        }

        public void DeleteCharacterByPosition(int index)
        {
            if (Value.Count > index && index > -1)
                Value.RemoveAt(index);
            else
                throw new IndexOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
        }


        public int CompareTo(object obj)
        {
            if (obj is Word)
                return string.Join(string.Empty, Value).CompareTo(string.Join(string.Empty, ((Word)obj).Value));
            return -1;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, Value);
        }

        public override bool Equals(object obj)
        {
            var word = obj as Word;
            return word != null &&
                   EqualityComparer<List<Character>>.Default.Equals(Value, word.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<List<Character>>.Default.GetHashCode(Value);
        }

        public int Compare(Word x, Word y)
        {
            if (x.Value.Count == y.Value.Count)
                return 0;
            else if (x.Value.Count < y.Value.Count)
                return -1;
            else
                return 1;
        }

        public bool Equals(Word other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Join(string.Empty, Value) == string.Join(string.Empty, other.Value);
        }
    }
}
