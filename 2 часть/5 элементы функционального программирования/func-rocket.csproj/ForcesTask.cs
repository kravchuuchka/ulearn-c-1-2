using System.Drawing;
using System.Linq;

namespace func_rocket
{
	public class ForcesTask
    {
        public static RocketForce GetThrustForce(double forceValue)
		{
			return r => new Vector(forceValue, 0).Rotate(r.Direction);
		}

		public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
		{
			return r => gravity(spaceSize, r.Location);
		}

		public static RocketForce Sum(params RocketForce[] forces)
		{
			return r =>
			{
				var result = Vector.Zero;
				foreach (var item in forces)
					result += item(r);
				return result;
			};
		}
	}
}