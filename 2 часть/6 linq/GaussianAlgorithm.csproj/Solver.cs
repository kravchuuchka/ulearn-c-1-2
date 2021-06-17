using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] Solve(double[][] items, double[] freeMembers)
        {
            var matrix = new List<List<double>>();
            var free = new List<double>();
            matrix = new List<List<double>>(items.Select(x => x.ToList()));
            free.AddRange(freeMembers.ToList());
            int changeColumn = 0;
            for (int i = 0; i < matrix[0].Count; i++)
                if (changeColumn + i < matrix[0].Count && i < matrix.Count)
                {
                    var mainCoefficient = matrix[i][i + changeColumn];
                    if (mainCoefficient == 0)
                    {
                        var possibleLine = matrix.FindIndex(i, x => x[i + changeColumn] != 0);
                        if (possibleLine == -1)
                        {
                            changeColumn++;
                            i--;
                            continue;
                        }
                        var temp = matrix[i];
                        matrix[i] = matrix[possibleLine];
                        matrix[possibleLine] = temp;
                        mainCoefficient = matrix[i][i + changeColumn];
                        var t2 = free[i];
                        free[i] = free[possibleLine];
                        free[possibleLine] = t2;
                    }
                    for (int y = 0; y < matrix[0].Count; y++)
                        matrix[i][y] = matrix[i][y] / mainCoefficient;
                    free[i] = free[i] / mainCoefficient;
                    for (int x = 0; x < matrix.Count; x++)
                        if (x != i)
                        {
                            var tempCoefficient = matrix[x][i + changeColumn];
                            for (int y = 0; y < matrix[0].Count; y++)
                                matrix[x][y] = matrix[x][y] - matrix[i][y] * tempCoefficient;
                            free[x] = free[x] - free[i] * tempCoefficient;
                            if (matrix[x].All(z => Math.Abs(z) < 1e-3) && Math.Abs(free[x]) > 1e-3)
                                throw new NoSolutionException("There is no decision " + free[x] + " " + x);
                        }
                }
            var result = new List<double>();
            changeColumn = 0;
            for (int i = 0; i < matrix[0].Count; i++)
                if (i - changeColumn >= free.Count)
                    result.Add(0);
                else if (matrix[i - changeColumn][i] == 0)
                {
                    result.Add(0);
                    changeColumn++;
                }
                else
                    result.Add(free[i - changeColumn] / matrix[i - changeColumn][i]);
            return result.ToArray();
        }
    }
}
