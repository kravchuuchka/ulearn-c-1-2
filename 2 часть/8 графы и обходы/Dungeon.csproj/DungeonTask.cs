using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();
            if (pathToExit == null)
                return new MoveDirection[0];
            if (map.Chests.Any(chest => pathToExit.ToList().Contains(chest)))
                return pathToExit.ToList().ParseToDirections();
            var chests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
            var result = chests.Select(chest => Tuple.Create(
                chest, BfsTask.FindPaths(map, chest.Value, new[] { map.Exit }).FirstOrDefault()))
                .MinElement();
            if (result == null) return pathToExit.ToList().ParseToDirections();
            return result.Item1.ToList().ParseToDirections().Concat(
                result.Item2.ToList().ParseToDirections())
                .ToArray();
        }
    }

    public static class ExtentionMetods
    {
        public static Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>
            MinElement(this IEnumerable<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> items)
        {
            if (items.Count() == 0 || items.First().Item2 == null)
                return null;

            var min = int.MaxValue;
            var minElement = items.First();
            foreach (var element in items)
                if (element.Item1.Length + element.Item2.Length < min)
                {
                    min = element.Item1.Length + element.Item2.Length;
                    minElement = element;
                }
            return minElement;
        }

        public static MoveDirection[] ParseToDirections(this List<Point> items)
        {
            var resultList = new List<MoveDirection>();
            if (items == null)
                return new MoveDirection[0];
            var itemsLength = items.Count;

            for (var i = itemsLength - 1; i > 0; i--)
            {
                resultList.Add(Walker.ConvertOffsetToDirection
                    (new Size(items[i - 1].X - items[i].X, items[i - 1].Y - items[i].Y)));
            }
            return resultList.ToArray();
        }
    }
}
