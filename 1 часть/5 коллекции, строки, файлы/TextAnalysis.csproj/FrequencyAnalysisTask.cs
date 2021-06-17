using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var ngrams = GetNgrams(text);
            foreach (var word in ngrams)
            {
                var frequentLastWord = GetMostFrequentLastWords(word.Value);
                result.Add(word.Key, frequentLastWord);
            }

            return result;
        }

        private static string GetMostFrequentLastWords(Dictionary<string, int> ngrams)
        {
            var result = "";
            var maxValue= -1;
            foreach (var lastWord in ngrams)
            {
                if (lastWord.Value > maxValue)
                {
                    maxValue = lastWord.Value;
                    result = lastWord.Key;
                }
                else
                {
                    if (lastWord.Value == maxValue && string.CompareOrdinal(lastWord.Key, result) < 0)
                    {
                        result = lastWord.Key;
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> GetNgrams(List<List<string>> text)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var words in text)
            {
                for (int i = 0; i < words.Count - 1; i++)
                {
                    var key = words[i];
                    var value = words[i + 1];
                    ChangeResult(key, value, result);

                    if (i < words.Count - 2)
                    {
                        key = words[i] + " " + words[i + 1];
                        value = words[i + 2];
                        ChangeResult(key, value, result);
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> ChangeResult(string key, string value, 
        Dictionary<string, Dictionary<string, int>> result)
        {
            if (!result.ContainsKey(key))
                result.Add(key, new Dictionary<string, int>());

            if (!result[key].ContainsKey(value))
                result[key].Add(value, 0);

            result[key][value]++;

            return result;
        }
    }
}

