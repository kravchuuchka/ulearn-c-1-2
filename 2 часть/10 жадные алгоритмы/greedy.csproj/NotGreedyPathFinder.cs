using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;
using System;

namespace Greedy
{
	class PathWithTarget
	{
		public List<Point> Path { get; set; }
		public int NumberOfTargets { get; set; }

		public PathWithTarget(List<Point> path, int numberOfTargets)
		{
			Path = path;
			NumberOfTargets = numberOfTargets;
		}
	}

	public class NotGreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
			var notVisitedChests = state.Chests.ToList();
			var position = state.Position;
			var resultPath = new PathWithCost(0, position);
			var bestPath = new PathWithTarget(new List<Point>(), state.Chests.Count);
			CreatePath(state, position, notVisitedChests, resultPath, bestPath);
			return bestPath.Path.Skip(1).ToList();
		}

		private static void CreatePath(State state, Point start, List<Point> notVisitedTargets,
	    PathWithCost resultPath, PathWithTarget bestPath)
		{
			CorrectBestPath(bestPath, resultPath, notVisitedTargets);
			var pathFinder = new DijkstraPathFinder();
			var paths = pathFinder.GetPathsByDijkstra(state, start, notVisitedTargets);
			foreach (var e in paths)
			{
				var cost = resultPath.Cost + e.Cost;
				if (cost <= state.Energy)
				{
					notVisitedTargets.Remove(e.End);
					var way = resultPath.Path.ToList();
					way.AddRange(e.Path.Skip(1));
					CreatePath(state, e.End, notVisitedTargets, new PathWithCost(cost, way.ToArray()), bestPath);
					if (bestPath.NumberOfTargets == 0) 
						return;
					notVisitedTargets.Add(e.End);
				}
			}
		}

		private static void CorrectBestPath(PathWithTarget bestPath, PathWithCost resultPath,
		List<Point> notVisitedTargets)
		{
			if (bestPath.NumberOfTargets > notVisitedTargets.Count)
			{
				bestPath.Path = resultPath.Path.ToList();
				bestPath.NumberOfTargets = notVisitedTargets.Count;
			}
		}
	}
}