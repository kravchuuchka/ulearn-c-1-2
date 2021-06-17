using System.ComponentModel;
using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            width -= 2;
            height -= 2;

            if (height < width)
                DiagonalMove(robot, width / height + 1, 2, height, Direction.Right, Direction.Down);
            else
                DiagonalMove(robot, (int)Math.Round((double)height / width) + 1, 2, width, Direction.Down, Direction.Right);
        }

        public static void DiagonalMove(Robot r, int gap1, int gap2, int end, Direction d1, Direction d2)
        {
            for (int i = 0; i < end; i++)
            {
                Move(r, gap1 - 1, d1);
                if (!r.Finished)
                    Move(r, gap2 - 1, d2);
            }
        }

        public static void Move(Robot robot, int steps, Direction direction)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(direction);
        }
    }
}
