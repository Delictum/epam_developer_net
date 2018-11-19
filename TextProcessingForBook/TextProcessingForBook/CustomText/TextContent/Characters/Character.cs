using System;

namespace TextProcessingForBook.CustomText.TextContent.Characters
{
    public class Character : IComparable
    {
        public char Value { get; }
        public CharacterType Type { get; }


        public Character (char setCharacter)
        {
            Value = setCharacter;
            Type = SetType(setCharacter);
        }


        private CharacterType SetType(char setCharacter)
        {
            if (CharacterHelper.IsEndPunctuation(setCharacter))
                return CharacterType.EndPunctuation;
            else if (CharacterHelper.IsMidPunctuation(setCharacter))
                return CharacterType.MidPunctuation;
            else if (CharacterHelper.IsCloseOpenPunctuation(setCharacter))
                return CharacterType.CloseOpenPunctuation;
            else if (char.IsDigit(setCharacter))
                return CharacterType.Digit;
            else if (CharacterHelper.IsVowel(setCharacter))
                return CharacterType.Vowel;
            else if (CharacterHelper.IsConsonant(setCharacter))
                return CharacterType.Consonant;
            else if (char.IsWhiteSpace(setCharacter))
                return CharacterType.WhiteSpace;
            else if (char.IsControl(setCharacter))
                return CharacterType.Control;
            else if (CharacterHelper.IsMath(setCharacter))
                return CharacterType.Math;
            else
                return CharacterType.Special;
        }


        public int CompareTo(object obj)
        {
            if (obj is Character)
                return Value.CompareTo(((Character)obj).Value);
            return -1;
        }

        public override string ToString()
        {
            return char.ToString(Value);
        }

        public override bool Equals(object obj)
        {
            var character = obj as Character;
            return character != null &&
                   Value == character.Value &&
                   Type == character.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1574892647;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}