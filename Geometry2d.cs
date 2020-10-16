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
        static public float Slope(float a_x, float a_y, float b_x, float b_y)
        {
            var dx = a_x - b_x;
            if (Math.Abs(dx) <= float.Epsilon) { return float.NaN; }
            return (a_y - b_y) / dx;
        }

        static public float Slope(Point<float> a, Point<float> b)
        {
            return Slope(a.X, a.Y, b.X, b.Y);
        }

        static public bool IsClockwise(float a_x, float a_y, float b_x, float b_y, float c_x, float c_y)
        {
            return (b_y - a_y) * (c_x - a_x) > (c_y - a_y) * (b_x - a_x);
        }

        static public bool IsClockwise(Point<float> a, Point<float> b, Point<float> c)
        {
            return IsClockwise(a.X, a.Y, b.X, b.Y, c.X, c.Y);
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
