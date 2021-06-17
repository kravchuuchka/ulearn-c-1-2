using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var passed = new HashSet<Point>();
            var queues = map.Players.Select(_ => new Queue<OwnedLocation>()).ToArray();
            for (var i = 0; i < map.Players.Length; i++)
            {
                var playerPosition = map.Players[i];
                var ownedLocation = new OwnedLocation(i, playerPosition, 0);
                passed.Add(playerPosition);
                queues[i].Enqueue(ownedLocation);
                yield return ownedLocation;
            }

            while (queues.Any(x => x.Any()))
                foreach (var queue in queues)
                    foreach (var ownedLocation in DoNextMove(map, passed, queue))
                        yield return ownedLocation;
        }

        private static IEnumerable<OwnedLocation> DoNextMove(
            Map map, HashSet<Point> visited, Queue<OwnedLocation> queue)
        {
            var count = queue.Count();
            for (var i = 0; i < count; i++)
            {
                var ownedLocation = queue.Dequeue();
                var nextPositions = GetPossibleNextPositions(map, visited, ownedLocation.Location);
                foreach (var position in nextPositions)
                {
                    var nextOwnedLocation =
                        new OwnedLocation(ownedLocation.Owner, position, ownedLocation.Distance + 1);
                    if (ownedLocation.Distance >= nextOwnedLocation.Distance)
                        continue;
                    visited.Add(nextOwnedLocation.Location);
                    queue.Enqueue(nextOwnedLocation);
                    yield return nextOwnedLocation;
                }
            }
        }

        private static IEnumerable<Point> GetPossibleNextPositions(
            Map map, HashSet<Point> visited, Point currentPosition)
        {
            var possibleDirections = new List<Size>
            {
                new Size(-1, 0), 
                new Size(0, -1),
                new Size(0, 1), 
                new Size(1, 0)
            };

            return possibleDirections
                .Select(direction => currentPosition + direction)
                .Where(map.InBounds)
                .Where(point => map.Maze[point.X, point.Y] == MapCell.Empty)
                .Where(point => !visited.Contains(point));
        }
    }
}
