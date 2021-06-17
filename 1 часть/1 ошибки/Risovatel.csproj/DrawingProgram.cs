using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphics;

        public static void Initialize(Graphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        { 
            x = x0; 
            y = y0; 
        }

        public static void DrawTrajectory(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ChangeTrajectory(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        const float SideCoefficient = 0.375f;
        const float BevelCoefficient = 0.04f;
        public static void Draw(int width, int height, double angleOfRotation, Graphics graphics)
        {
            Drawer.Initialize(graphics);

            var size = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (size * SideCoefficient + size * BevelCoefficient) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drawer.SetPosition(x0, y0);
            DrawSide(size, 0);
            DrawSide(size, -Math.PI / 2);
            DrawSide(size, Math.PI);
            DrawSide(size, Math.PI / 2);
        }

        private static void DrawSide(int size, double angle)
        {
            Drawer.DrawTrajectory(Pens.Yellow, size * SideCoefficient, angle);
            Drawer.DrawTrajectory(Pens.Yellow, size * BevelCoefficient * Math.Sqrt(2), angle + Math.PI/4);
            Drawer.DrawTrajectory(Pens.Yellow, size * SideCoefficient, angle + Math.PI);
            Drawer.DrawTrajectory(Pens.Yellow, size * SideCoefficient - size * BevelCoefficient, angle + Math.PI/2);
        
            Drawer.ChangeTrajectory(size * BevelCoefficient, angle - Math.PI);
            Drawer.ChangeTrajectory(size * BevelCoefficient * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }
    }
}