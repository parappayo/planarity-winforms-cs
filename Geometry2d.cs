using System;

namespace BushidoBurrito.Planarity
{
    public struct Point<T>
    {
        public T X;
        public T Y;

        public Point(T x, T y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public struct Line<T>
    {
        public T YIntercept;
        public T Slope;
    }

    public class Geometry2d
    {
        static public float Slope(Point<float> a, Point<float> b)
        {
            var dx = a.X - b.X;
            if (Math.Abs(dx) <= float.Epsilon) { return float.NaN; }
            return (a.Y - b.Y) / dx;
        }

        static public bool IsClockwise(Point<float> a, Point<float> b, Point<float> c)
        {
            return (b.Y - a.Y) * (c.X - a.X) > (c.Y - a.Y) * (b.X - a.X);
        }

        static public float ValueAtX(Line<float> line, float x)
        {
            return line.YIntercept + line.Slope * x;
        }

        static public float IntersectionX(Line<float> a, Line<float> b)
        {
            float delta_y_intercept = a.YIntercept - b.YIntercept;
            float delta_slope = a.Slope - b.Slope;
            if (Math.Abs(delta_slope) < float.Epsilon) { return float.NaN; }
            return -delta_y_intercept / delta_slope;
        }
    }
}
