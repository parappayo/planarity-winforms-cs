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

    public struct LineSegment<T>
    {
        public Point<T> A;
        public Point<T> B;
    }

    public class Geometry2d
    {
        static public bool NearlyEqual(float a, float b, float epsilon)
        {
            const double MinNormal = 2.2250738585072014E-308d;

            if (a.Equals(b)) { return true; }

            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (absA < float.Epsilon ||
                absB < float.Epsilon ||
                absA + absB < MinNormal)
            {
                return diff < epsilon * MinNormal;
            }

            return diff / (absA + absB) < epsilon;
        }

        static public bool Equal(Point<float> a, Point<float> b)
        {
            const float epsilon = 0.001F;

            return NearlyEqual(a.X, b.X, epsilon) &&
                NearlyEqual(a.Y, b.Y, epsilon);
        }

        static public bool Equal(Line<float> a, Line<float> b)
        {
            const float epsilon = 0.001F;

            return NearlyEqual(a.YIntercept, b.YIntercept, epsilon) &&
                NearlyEqual(a.Slope, b.Slope, epsilon);
        }

        static public float Slope(Point<float> a, Point<float> b)
        {
            var dx = a.X - b.X;
            if (Math.Abs(dx) <= float.Epsilon) { return float.NaN; }
            return (a.Y - b.Y) / dx;
        }

        static public float Slope(LineSegment<float> line)
        {
            return Slope(line.A, line.B);
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

        static public Point<float> IntersectionPoint(Line<float> a, Line<float> b)
        {
            float x = IntersectionX(a, b);
            if (float.IsNaN(x)) { return new Point<float>(float.NaN, float.NaN); }
            float y = ValueAtX(a, x);
            return new Point<float>(x, y);
        }

        static public bool SharesPoint(LineSegment<float> a, LineSegment<float> b)
        {
            return Equal(a.A, b.A) ||
                Equal(a.A, b.B) ||
                Equal(a.B, b.A) ||
                Equal(a.B, b.B);
        }

        static public bool Intersects(LineSegment<float> a, LineSegment<float> b)
        {
            // not correct, but close enough for Planarity gameplay
            if (SharesPoint(a, b)) { return false; }

            return (IsClockwise(a.A, b.A, b.B) != IsClockwise(a.B, b.A, b.B)) &&
                (IsClockwise(a.A, a.B, b.A) != IsClockwise(a.A, a.B, b.B));
        }
    }
}
