using System.Linq;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, 
        string phraseBeginning, int wordsCount)
        {
            var words = phraseBeginning.Split(' ').ToList();
            var word1 = words[words.Count - 1];
            var word2 = "";
            string key;
            if (words.Count > 1) word2 = words[words.Count - 2];
            for (int i = 0; i < wordsCount; i++)
            {
                if (nextWords.ContainsKey(word2 + " " + word1))
                {
                    key = nextWords[word2 + " " + word1];
                    phraseBeginning += " " + key;
                    word2 = word1; word1 = key;
                }
                else if  (nextWords.ContainsKey(word1))
                {
                    key = nextWords[word1];
                    phraseBeginning += " " + key;
                    word2 = word1; word1 = key;
                }
                else break;
            }
            return phraseBeginning;
        }
    }
}