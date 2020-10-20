using System;
using System.Collections.Generic;

namespace BushidoBurrito.Planarity
{
    public class Intersection
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

        private List<Point<float>> FindIntersectionPoints(
            Line<float> line,
            List<Intersection> intersections)
        {
            var result = new List<Point<float>>();

            foreach (var i in intersections)
            {
                if (i.IsOnLine(line))
                {
                    result.Add(i.Point);
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
            var visitedPoints = new Dictionary<Point<float>, Pip>();

            foreach (var line in lines)
            {
                var intersectionPoints = FindIntersectionPoints(line, intersections);

                Pip previous = null;

                foreach (var point in intersectionPoints)
                {
                    Pip pip = null;

                    if (visitedPoints.ContainsKey(point))
                    {
                        pip = visitedPoints[point];
                    }
                    else
                    {
                        pip = new Pip(point);
                        visitedPoints[point] = pip;
                    }

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

            pips.AddRange(visitedPoints.Values);
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
