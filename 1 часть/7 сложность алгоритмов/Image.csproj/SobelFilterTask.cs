using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];

            var biasX = sx.GetLength(0) / 2;
            var biasY = sx.GetLength(1) / 2;
            var sy = GetTransposedMatrix(sx);

            for (var x = biasX; x < width - biasX; x++)
                for (var y = biasY; y < height - biasY; y++)
                {
                    var gx = GetConvolution(g, sx, x, y, biasX);
                    var gy = GetConvolution(g, sy, x, y, biasY);

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }

            return result;
        }

        private static double[,] GetTransposedMatrix(double[,] sx)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            var transposedMatrix = new double[width, height];

            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    transposedMatrix[x, y] = sx[y, x];

            return transposedMatrix;
        }

        private static double GetConvolution(double[,] g, double[,] sVariable, int x, int y, int biasVariable)
        {
            var width = sVariable.GetLength(0);
            var height = sVariable.GetLength(1);
            var convolution = 0.0;

            for (var sx = 0; sx < width; sx++)
                for (var sy = 0; sy < height; sy++)
                    convolution += sVariable[sx, sy] * g[x + sx - biasVariable, y + sy - biasVariable];

            return convolution;
        }
    }
}