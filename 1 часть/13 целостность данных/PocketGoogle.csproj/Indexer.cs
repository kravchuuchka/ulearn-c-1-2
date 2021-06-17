using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly char[] wordsSplitters = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };

        private readonly Dictionary<int, Dictionary<string, List<int>>> indicesInfo = new Dictionary<int, Dictionary<string, List<int>>>();

        private readonly Dictionary<string, HashSet<int>> indicesWordsByID = new Dictionary<string, HashSet<int>>();

        public void Add(int id, string document)
        {
            var splittedWords = document.Split(wordsSplitters);
            indicesInfo.Add(id, new Dictionary<string, List<int>>());
            var startNextWord = 0;
            foreach (var word in splittedWords)
            {
                if (!indicesWordsByID.ContainsKey(word)) indicesWordsByID[word] = new HashSet<int>();
                if (!indicesWordsByID[word].Contains(id)) indicesWordsByID[word].Add(id);
                if (!indicesInfo[id].ContainsKey(word)) indicesInfo[id].Add(word, new List<int>());
                indicesInfo[id][word].Add(startNextWord);
                startNextWord += word.Length + 1;
            }
        }

        public List<int> GetIds(string word) =>
            indicesWordsByID.ContainsKey(word) ? indicesWordsByID[word].ToList() : new List<int>();

        public List<int> GetPositions(int id, string word) =>
            indicesInfo.ContainsKey(id) && indicesInfo[id].ContainsKey(word) ? indicesInfo[id][word] : new List<int>();

        public void Remove(int id)
        {
            string[] words = indicesInfo[id].Keys.ToArray();
            indicesInfo.Remove(id);
            foreach (var word in words)
                indicesWordsByID[word].Remove(id);
        }
    }
}

