
namespace BushidoBurrito.Planarity
{
    public struct Intersection
    {
        public Line<float> FromLine;
        public Line<float> ToLine;
        public Point<float> Point;

        public Intersection(Line<float> fromLine, Line<float> toLine)
        {
            FromLine = fromLine;
            ToLine = toLine;
            Point = Geometry2d.IntersectionPoint(fromLine, toLine);
        }

        public bool IsValid()
        {
            return !float.IsNaN(Point.X) &&
                !float.IsNaN(Point.Y);
        }

        public bool IsOnLine(Line<float> line)
        {
            return IsValid() &&
                (Geometry2d.Equal(line, FromLine) ||
                Geometry2d.Equal(line, ToLine));
        }
    }
}
