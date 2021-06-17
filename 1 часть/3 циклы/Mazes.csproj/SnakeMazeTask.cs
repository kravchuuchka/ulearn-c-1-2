using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int i = 0;
            while (!robot.Finished)
            {
                MoveSnake(robot, i, width - 3);
                i++;
                if (!robot.Finished)
                    Move(robot, 2, Direction.Down);
            }
        }

        public static void MoveSnake(Robot robot, int countRepeat, int steps)
        {
            if (countRepeat % 2 == 0)
                Move(robot, steps, Direction.Right);
            else
                Move(robot, steps, Direction.Left);
        }

        public static void Move(Robot robot, int steps, Direction direction)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(direction);
        }
    }
}
