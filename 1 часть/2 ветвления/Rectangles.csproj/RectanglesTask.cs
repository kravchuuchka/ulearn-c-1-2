using System;
using System.Drawing.Text;

namespace Rectangles
{
	public static class RectanglesTask
	{
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			return (r1.Left <= r2.Right) && (r1.Top <= r2.Bottom) && (r1.Right >= r2.Left) && (r1.Bottom >= r2.Top);
		}

		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			int left = Math.Max(r1.Left, r2.Left);
			int top = Math.Max(r1.Top, r2.Top);
			int right = Math.Min(r1.Right, r2.Right);
			int bottom = Math.Min(r1.Bottom, r2.Bottom);

			int height = bottom - top;
			int width = right - left;
            	
			if ((height < 0) || (width < 0))
				return 0;
			return height * width;
		}

		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if (IsInnerRectangle(r1, r2))
				return 0;
			if (IsInnerRectangle(r2, r1))
				return 1;
			return -1;
		}
		public static bool IsInnerRectangle(Rectangle x1, Rectangle x2)
		{
			return (x1.Right <= x2.Right) && (x1.Bottom <= x2.Bottom) && (x1.Left >= x2.Left) && (x1.Top >= x2.Top);
		}
	}
}