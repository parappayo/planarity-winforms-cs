using System;
using System.Collections.Generic;

namespace BushidoBurrito.Planarity
{
    /// <summary>
    /// Draggable game piece, a dot
    /// </summary>
    public class Pip
    {
        public float X;
        public float Y;

        public Pip(Point<float> p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point<float> Point()
        {
            return new Point<float>(X, Y);
        }
    }

    /// <summary>
    /// An edge in the graph theory sense
    /// </summary>
    public class Edge<T> where T : class
    {
        public T From;
        public T To;

        static public List<T> Flatten(List<Edge<T>> edges)
        {
            var result = new List<T>();

            foreach (var edge in edges)
            {
                result.Add(edge.From);
                result.Add(edge.To);
            }

            return result;
        }
    }

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

    public class LevelGenerator
    {
        public List<Pip> Pips;
        public List<Edge<Pip>> Connections;

        private Line<float> RandomLine(UniqRandInt yIntercepts, UniqRandInt slopes)
        {
            return new Line<float>()
            {
                YIntercept = yIntercepts.Next(-1000, 1000),
                Slope = slopes.Next(-200, 200)
            };
        }

        private void GenerateIntersections(
            int lineCount,
            List<Line<float>> lines,
            List<Intersection> intersections)
        {
            var random = new Random();
            var yIntercepts = new UniqRandInt(random);
            var slopes = new UniqRandInt(random);

            for (int i = 0; i < lineCount; i++)
            {
                var line = RandomLine(yIntercepts, slopes);

                foreach (var oldLine in lines)
                {
                    var intersection = new Intersection(oldLine, line);

                    if (intersection.IsValid())
                    {
                        intersections.Add(intersection);
                    }
                }

                lines.Add(line);
            }
        }

        private List<Pip> FindPipsAlongLine(
            Line<float> line,
            List<Intersection> intersections)
        {
            var result = new List<Pip>();

            foreach (var i in intersections)
            {
                if (i.IsOnLine(line))
                {
                    result.Add(new Pip(i.Point));
                }
            }

            return result;
        }

        private void FindPips(
            List<Line<float>> lines,
            List<Intersection> intersections,
            List<Pip> pips,
            List<Edge<Pip>> connections)
        {
            foreach (var line in lines)
            {
                var pipsOnLine = FindPipsAlongLine(line, intersections);

                Pip previous = null;

                foreach (var pip in pipsOnLine)
                {
                    pips.Add(pip);

                    if (previous != null)
                    {
                        connections.Add(new Edge<Pip>()
                        {
                            From = previous,
                            To = pip
                        });
                    }

                    previous = pip;
                }
            }
        }

        public void GenerateLevel(int lineCount)
        {
            var lines = new List<Line<float>>();
            var intersections = new List<Intersection>();
            var pips = new List<Pip>();
            var connections = new List<Edge<Pip>>();

            GenerateIntersections(lineCount, lines, intersections);
            FindPips(lines, intersections, pips, connections);

            Pips = pips;
            Connections = connections;
        }
    }
}
