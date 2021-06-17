using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
	public class DijkstraData
	{
		public Point? Previous { get; set; }
		public int Price { get; set; }
	}

	public class DijkstraPathFinder
	{
		public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
		{
			HashSet<Point> chests = new HashSet<Point>(targets); 
			HashSet<Point> passedPeaks = new HashSet<Point>();
			HashSet<Point> candidatesOpening = new HashSet<Point>();
			var track = new Dictionary<Point, DijkstraData>(); 
			track[start] = new DijkstraData { Previous = null, Price = 0 };
			candidatesOpening.Add(start);
			return MakeDijkstra(state, chests, passedPeaks, candidatesOpening, track);
		}

		public static IEnumerable<PathWithCost> MakeDijkstra(State state, HashSet<Point> chests, HashSet<Point> passedPeaks,
HashSet<Point> candidatesOpening, Dictionary<Point, DijkstraData> track)
		{
			while (candidatesOpening.Count != 0)
			{
				Point? toOpen = null;
				var bestPrice = int.MaxValue;
				foreach (var e in candidatesOpening)
					if (track[e].Price < bestPrice)
					{
						bestPrice = track[e].Price;
						toOpen = e;
					}
				if (toOpen == null) yield break;
				if (chests.Contains(toOpen.Value)) yield return MakePath(track, toOpen.Value);
				FindNextPeaksDijkstra(state, chests, passedPeaks, candidatesOpening, track, toOpen);
			}
		}

		public static PathWithCost MakePath(Dictionary<Point, DijkstraData> track, Point end)
		{
			var result = new List<Point>();
			Point? currentPoint = end;
			while (currentPoint != null)
			{
				result.Add(currentPoint.Value);
				currentPoint = track[currentPoint.Value].Previous;
			}
			result.Reverse();
			PathWithCost pathResult = new PathWithCost(track[end].Price, result.ToArray());
			return pathResult;
		}

		public static IEnumerable<Point> GetIncidentPeaks(Point node, State state)
		{
			return new Point[]
			{
				new Point(node.X, node.Y + 1),
				new Point(node.X, node.Y - 1),
				new Point(node.X + 1, node.Y),
				new Point(node.X - 1, node.Y)
			}.Where(point => state.InsideMap(point) && !state.IsWallAt(point));
		}

		public static void FindNextPeaksDijkstra(State state, HashSet<Point> chests, HashSet<Point> passedPeaks,
HashSet<Point> candidatesOpening, Dictionary<Point, DijkstraData> track, Point? toOpen)
        {
			var incidentPeaks = GetIncidentPeaks(toOpen.Value, state);
			foreach (var e in incidentPeaks)
			{
				var currentPrice = track[toOpen.Value].Price + state.CellCost[e.X, e.Y];
				if (!track.ContainsKey(e) || track[e].Price > currentPrice)
					track[e] = new DijkstraData { Previous = toOpen, Price = currentPrice };
				if (!passedPeaks.Contains(e))
					candidatesOpening.Add(e);
			}
			candidatesOpening.Remove(toOpen.Value);
			passedPeaks.Add(toOpen.Value);
		}
	}
}
