using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var numbers = GetCountByPrefix(phrases, prefix);
            if (numbers <= 0)
                return new string[0];
            if (count > numbers)
                count = numbers;
            var words = new string[count];
            for (var i = 0; i < count; i++)
                words[i] = phrases[startIndex + i];
            return words;
        }

        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new List<string>();
            var result = AutocompleteTask.GetCountByPrefix(phrases, "a");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>() {"Hello", "my", "name", "is", "Nastya"};
            var totalCount = phrases.Count;
            var result = AutocompleteTask.GetCountByPrefix(phrases, "");
            Assert.AreEqual(totalCount, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenAllPhrasesContainPrefix()
        {
            var phrases = new List<string>() {"be", "beautiful", "beach", "bee", "bear", "believe"};
            var totalCount = phrases.Count;
            var result = AutocompleteTask.GetCountByPrefix(phrases, "be");
            Assert.AreEqual(totalCount, result);
        }

        [Test]
        public void CountByPrefix_IsG_WhenHaveNEntries()
        {
            var phrases = new List<string>() {"count", "coffee", "mother", "cat", "covid"};
            var g = 3;
            var result = AutocompleteTask.GetCountByPrefix(phrases, "co");
            Assert.AreEqual(g, result);
        }

        [Test]
        public void CountByPrefix_IsZero_WhenNoEntries()
        {
            var phrases = new List<string>() {"flower", "tree", "apple", "banana"};
            var result = AutocompleteTask.GetCountByPrefix(phrases, "juice");
            Assert.AreEqual(0, result);
        }
    }
}
