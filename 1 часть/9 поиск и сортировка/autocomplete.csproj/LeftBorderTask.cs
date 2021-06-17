using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left == right - 1) 
                return left;
            var middle = (right + left) / 2;
            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) < 0
            || phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return GetLeftBorderIndex(phrases, prefix, left, middle);

            return GetLeftBorderIndex(phrases, prefix, middle, right);
        }
    }
}

