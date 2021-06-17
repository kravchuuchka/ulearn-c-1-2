using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
		static readonly Rocket rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
		static readonly Vector target = new Vector(600, 200);

		public static IEnumerable<Level> CreateLevels()
		{
			yield return GenerateLevel("Zero", target, (size, v) => Vector.Zero);
			yield return GenerateLevel("Heavy", target, (size, v) => new Vector(0, 0.9));
			yield return GenerateLevel("Up", new Vector(700, 500), (size, v) =>
			{
				var d = size.Height - v.Y;
				return new Vector(0, -300 / (d + 300.0));
			});
			yield return GenerateLevel("WhiteHole", target, (size, v) =>
				CountGravityTarget(v));
			yield return GenerateLevel("BlackHole", target, (size, v) =>
				CountGravityAnomaly(v));
			yield return GenerateLevel("BlackAndWhite", target, (size, v) =>
			    (CountGravityTarget(v) + CountGravityAnomaly(v)) / 2);
		}

		public static Level GenerateLevel(string name, Vector target, Gravity gravity)
        {
			return new Level(name, rocket, target, gravity, standardPhysics);
        }

		public static Vector CountGravityTarget(Vector v)
        {
			var dTarget = v - target;
			return 140 * dTarget / (dTarget.Length * dTarget.Length + 1);
		}

		public static Vector CountGravityAnomaly(Vector v)
        {
			var dAnomaly = (target + rocket.Location) / 2 - v;
			return 300 * dAnomaly / (dAnomaly.Length * dAnomaly.Length + 1);
		}
    }
}