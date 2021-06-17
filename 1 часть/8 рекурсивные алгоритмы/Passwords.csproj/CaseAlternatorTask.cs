using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                result.Add(new string(word));
                return;
            }

            char lower, upper;
            if (char.IsLetter(word[startIndex]))
            {
                lower = char.ToLower(word[startIndex]);
                upper = char.ToUpper(word[startIndex]);
            }
            else
                lower = upper = word[startIndex];

            word[startIndex] = lower;

            AlternateCharCases(word, startIndex + 1, result);

            if (lower != upper)
            {
                word[startIndex] = upper;
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}