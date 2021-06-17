using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\\", 0, @"\", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var openingQuote = line[startIndex];
            var builder = new StringBuilder();
            var i = startIndex + 1;

            while (i < line.Length && line[i] != openingQuote)
            {
                if (line[i] == '\\')
                {
                    i++;
                }
                if (i < line.Length)
                {
                    builder.Append(line[i]);
                }
                i++;
            }
            if (i == line.Length)
            {
                return new Token(builder.ToString(), startIndex, i - startIndex);
            }
            return new Token(builder.ToString(), startIndex, i - startIndex + 1);
        }
    }
}
