using System.Linq;

namespace TextProcessingForBook.CustomText.TextContent.Characters
{
    public static class CharacterHelper
    {
        public static bool IsEndPunctuation(char ch)
        {
            return ".!?".Any(c => c == ch);
        }

        public static bool IsMidPunctuation(char ch)
        {
            return "-:;,".Any(c => c == ch);
        }

        public static bool IsCloseOpenPunctuation(char ch)
        {
            return "{[<\"'>]}".Any(c => c == ch);
        }

        public static bool IsVowel(char ch)
        {
            return "eyuioaEYUIOA".Any(c => c == ch);
        }

        public static bool IsConsonant(char ch)
        {
            return "qwrtpsdfghjklzxcvbnmQWRTPSDFGHJKLZXCVBNM".Any(c => c == ch);
        }

        public static bool IsMath(char ch)
        {
            return "+=*/%^".Any(c => c == ch);
        }
    }
}
