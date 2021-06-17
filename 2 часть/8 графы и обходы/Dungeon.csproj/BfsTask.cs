using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var queue = new Queue<Point>(); var passed = new HashSet<Point>();
            var options = new Dictionary<Point, SinglyLinkedList<Point>>();
            passed.Add(start); queue.Enqueue(start);
            options.Add(start, new SinglyLinkedList<Point>(start));
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (!map.InBounds(point)) continue;
                if (map.Dungeon[point.X, point.Y] != MapCell.Empty) continue;
                foreach (Size e in Walker.PossibleDirections)
                {
                    if (e.IsEmpty) continue;
                    var nextPoint = point + e;
                    if (passed.Contains(nextPoint)) continue;
                    queue.Enqueue(nextPoint);
                    passed.Add(nextPoint);
                    options.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, options[point]));
                    }
            }
            foreach (var e in chests)
                if (options.ContainsKey(e)) yield return options[e];
        }
    }
}