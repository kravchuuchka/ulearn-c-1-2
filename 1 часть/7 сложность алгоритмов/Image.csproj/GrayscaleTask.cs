namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var length = original.GetLength(0);
            var width = original.GetLength(1);
            var result = new double[length, width];

            for (int y = 0; y < width; y++)
                for (int x = 0; x < length; x++)
                {
                    var red = 0.299 * original[x, y].R;
                    var green = 0.587 * original[x, y].G;
                    var blue = 0.114 * original[x, y].B;
                    result[x, y] = (red + green + blue) / 255;
                }
            return result;
        }
    }
}