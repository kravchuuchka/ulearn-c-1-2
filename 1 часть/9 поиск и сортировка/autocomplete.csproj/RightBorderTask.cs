﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (phrases.Count == 0 || prefix == "")
                return right;
            while (right - left > 1)
            {
                var middle = (right + left) / 2;
                if (string.Compare(phrases[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0
                || phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = middle;
                else
                    right = middle;
            }
            return right;
        }
    }
}
