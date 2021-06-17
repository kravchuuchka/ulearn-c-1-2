using System;
using System.Configuration;
using System.Collections.Generic;
using DocumentTokens = System.Collections.Generic.List<string>;
using System.Linq;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var comparisonResultList = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i++)
                for (var j = i + 1; j < documents.Count; j++)
                {
                    comparisonResultList.Add(new ComparisonResult(documents[i],
                    documents[j], FindLevenshteinDistance(documents[i], documents[j])));
                }
            return comparisonResultList;
        }

        public static double FindLevenshteinDistance(DocumentTokens first, DocumentTokens second)
        {
            var currentString = new double[second.Count + 1];
            var previousString = new double[second.Count + 1];
            currentString[0] = 1;
            for (var i = 0; i <= second.Count; i++)
                previousString[i] = i;
            for (var i = 1; i <= first.Count; i++)
            {
                for (var j = 1; j <= second.Count; j++)
                {
                    if (first[i - 1] == second[j - 1])
                        currentString[j] = previousString[j - 1];
                    else 
                        currentString[j] = FindMin(previousString[j - 1] +
                    TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]), 1 + previousString[j], 1
                    + currentString[j - 1]);
                }

                for (var k = 0; k < currentString.Length; k++)
                    previousString[k] = currentString[k];
                currentString[0] = i + 1;
            }
            return currentString[second.Count];
        }

        public static double FindMin(params double[] values)
        {
            return values.Min();
        }
    }
}
