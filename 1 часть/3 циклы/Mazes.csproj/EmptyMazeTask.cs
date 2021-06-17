using System.ComponentModel;

namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			Move(robot, width - 2, Direction.Right);
			Move(robot, height - 2, Direction.Down);
		}

		public static void Move(Robot robot, int steps, Direction direction)
        {
			for (int i = 0; i < steps; i++)
				robot.MoveTo(direction);
        }
	}
}