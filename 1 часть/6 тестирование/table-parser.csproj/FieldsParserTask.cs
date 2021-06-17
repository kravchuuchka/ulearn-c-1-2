using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("hello  world", new[] { "hello", "world" })]
        [TestCase(" hello", new[] { "hello" })]
        [TestCase("", new string[0])]
        [TestCase(@" '' ", new[] { "" })]
        [TestCase("'hello' world", new[] { "hello", "world" })]
        [TestCase("hello 'world'", new[] { "hello", "world" })]
        [TestCase("'hello world'", new[] { "hello world" })]
        [TestCase(@"'""hello""", new[] { @"""hello""" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase(@"""a 'b' 'c' d""", new[] { "a 'b' 'c' d" })]
        [TestCase(@"a""b c d e""", new[] { "a", "b c d e" })]
        [TestCase("'hello ", new[] { "hello " })]
        [TestCase(@"'a\'a\'a'", new[] { "a'a'a" })]
        [TestCase(@"""hello \""world\""""", new[] { @"hello ""world""" })]

        public static void RunTests(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
    }
    

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var result = new List<Token>();
            var i = 0;
            while (i < line.Length)
            {
                var spaces = GetSpacesField(line, i);
                i = spaces.GetIndexNextToToken();
                if (i >= line.Length)
                {
                    break;
                }
                var field = ReadField(line, i);
                result.Add(field);
                i = field.GetIndexNextToToken();
            }
            return result; 
        }
        
        private static Token GetSpacesField(string line, int startIndex)
        {
            var i = startIndex;
            while (i < line.Length && line[i] == ' ')
            {
                i++;
            }
            return new Token("", startIndex, i - startIndex);
        }

        private static Token ReadField(string line, int startIndex)
        {
            var symbol = line[startIndex];
            if (symbol == '\'' || symbol == '"')
            {
                return ReadQuotedField(line, startIndex);
            }
            return ReadSimplefield(line, startIndex);
        }

        private static Token ReadSimplefield(string line, int startIndex)
        {
            var i = startIndex;
            var builder = new StringBuilder();
            while (i < line.Length && line[i] != ' ' && line[i] != '\'' && line[i] != '"')
            {
                builder.Append(line[i]);
                i++;
            }
            return new Token(builder.ToString(), startIndex, i - startIndex);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}