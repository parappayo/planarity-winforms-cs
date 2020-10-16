using System.Drawing;

namespace BushidoBurrito.Planarity
{
    public class Geometry2d
    {
        static public int Slope(Point a, Point b)
        {
            int dx = a.X - b.X;
            if (dx == 0) { return 0; }
            return (a.Y - b.Y) / dx;
        }
    }
}
