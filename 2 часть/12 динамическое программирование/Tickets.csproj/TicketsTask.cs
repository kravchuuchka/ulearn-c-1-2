using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    public class TicketsTask
    {
        public static BigInteger Solve(int totalLen, int totalSum)
        {
            if (totalSum % 2 != 0) 
                return 0;
            var halfSum = totalSum / 2;
            var opt = new BigInteger[totalLen + 1, halfSum + 1];
            for (int i = 1; i <= totalLen; i++) opt[i, 0] = 1;
            for (int i = 0; i <= halfSum; i++) opt[0, i] = 0;
            for (int i = 1; i <= totalLen; i++)
                for (int j = 1; j <= halfSum; j++)
                    opt[i, j] = j > i * 9 ? 0 : opt[i - 1, j] + opt[i, j - 1]
                        - (j > 9 ? opt[i - 1, j - 10] : 0);
            return opt[totalLen, halfSum] * opt[totalLen, halfSum];
        }
    }
}