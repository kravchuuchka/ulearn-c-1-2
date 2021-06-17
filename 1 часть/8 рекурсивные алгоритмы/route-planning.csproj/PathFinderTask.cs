using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var smallestDistance = double.PositiveInfinity;
            var order = new int[checkpoints.Length];
            var bestOrder = new int[checkpoints.Length];
            MakePermutation(order, 1, checkpoints, ref smallestDistance, bestOrder);
            return bestOrder;
        }

        public static int[] MakePermutation(int[] order, int position, Point[] checkpoints, 
        ref double smallesttDistance, int[] bestOrder)
        {
            var currentOrder = new int[position];
            Array.Copy(order, currentOrder, position);
            var pathLength = PointExtensions.GetPathLength(checkpoints, currentOrder);
            if (pathLength < smallesttDistance)
            {
                if (position == order.Length)
                {
                    smallesttDistance = pathLength;
                    bestOrder = (int[])order.Clone();
                    return order;
                }
                for (int i = 1; i < order.Length; i++)
                {
                    var index = Array.IndexOf(order, i, 0, position);
                    if (index != -1)
                        continue;
                    order[position] = i;
                    MakePermutation(order, position + 1, checkpoints, ref smallesttDistance, bestOrder);
                }
            }
            return order;
        }
    }
}
