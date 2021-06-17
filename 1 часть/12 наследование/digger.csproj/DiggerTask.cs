using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public static int X, Y = 0;
        private static int deltaX, deltaY = 0;

        public CreatureCommand Act(int x, int y)
        {
            X = x; Y = y;
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                    deltaY = 0; deltaX = -1;
                    break;
                case System.Windows.Forms.Keys.Up:
                    deltaY = -1; deltaX = 0;
                    break;
                case System.Windows.Forms.Keys.Right:
                    deltaY = 0; deltaX = 1;
                    break;

                case System.Windows.Forms.Keys.Down:
                    deltaY = 1; deltaX = 0;
                    break;
                default:
                    deltaX = 0; deltaY = 0;
                    break;
            }
            if (!(x + deltaX >= 0 && x + deltaX < Game.MapWidth && y + deltaY >= 0 && y + deltaY < Game.MapHeight))
            {
                deltaX = 0; deltaY = 0;
            }
            if (Game.Map[x + deltaX, y + deltaY] != null)
            {
                if (Game.Map[x + deltaX, y + deltaY].ToString() == "Digger.Sack")
                {
                    deltaX = 0; deltaY = 0;
                }
            }
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            if (neighbor == "Digger.Gold")
                Game.Scores += 10;
            return neighbor == "Digger.Sack" || neighbor == "Digger.Monster";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }


    public class Sack : ICreature
    {
        private int counter = 0;
        public static bool DeadlyForPlayer = false;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null || (counter > 0 && (map.ToString() == "Digger.Player" || 
                    map.ToString() == "Digger.Monster")))
                {
                    counter++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 }; ;
                }
            }

            if (counter > 1)
            {
                counter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            counter = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }


        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return neighbor == "Digger.Player" || neighbor == "Digger.Monster";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }


    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int deltaX = 0; int deltaY = 0;
            if (IsPlayerAlive())
            {
                if (Player.X == x)
                {
                    if (Player.Y < y) deltaY = -1;
                    else if (Player.Y > y) deltaY = 1;
                }
                else if (Player.Y == y)
                {
                    if (Player.X < x) deltaX = -1;
                    else if (Player.X > x) deltaX = 1;
                }
                else
                {
                    if (Player.X < x) deltaX = -1;
                    else if (Player.X > x) deltaX = 1;
                }
            }
            else return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            if (!(x + deltaX >= 0 && x + deltaX < Game.MapWidth && y + deltaY >= 0 && y + deltaY < Game.MapHeight))
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            var map = Game.Map[x + deltaX, y + deltaY];
            if (map != null)
                if (map.ToString() == "Digger.Terrain" || map.ToString() == "Digger.Sack" ||
                    map.ToString() == "Digger.Monster")
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return neighbor == "Digger.Sack" || neighbor == "Digger.Monster";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        static private bool IsPlayerAlive()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    var map = Game.Map[i, j];
                    if (map != null)
                    {
                        if (map.ToString() == "Digger.Player")
                        {
                            Player.X = i;
                            Player.Y = j;
                            return true;
                        }
                    }
                }
            return false;
        }
    }
}
