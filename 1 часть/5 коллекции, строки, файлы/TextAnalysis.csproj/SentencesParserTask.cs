using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var newText = text.ToLower();
            var sentences = newText.Split(".:;?!()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var sentence in sentences)
                GetListModifiedSentences(sentence, sentencesList);

            return sentencesList;
        }

        private static void AddWord(StringBuilder builder, List<string> wordsList)
        {
            if (builder.Length > 0)
            {
                wordsList.Add(builder.ToString());
                builder.Clear();
            }
        }

        private static List<List<string>> GetListModifiedSentences(string sentence, List<List<string>> sentencesList)
        {
            var builder = new StringBuilder();
            var wordsList = new List<string>();

            foreach (var symbol in sentence)
            {
                if (char.IsLetter(symbol) || symbol == '\'')
                    builder.Append(symbol);
                else
                    AddWord(builder, wordsList);
            }
            AddWord(builder, wordsList);
            if (wordsList.Count > 0)
                sentencesList.Add(wordsList);

            return sentencesList;
        }
    }
}