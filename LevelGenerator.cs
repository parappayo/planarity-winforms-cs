using System;
using System.Collections.Generic;

namespace BushidoBurrito.Planarity
{
    public class LevelGenerator
    {
        public List<Point<float>> Points;
        public List<LineSegment<float>> Connections;

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

        private List<Point<float>> FindPointsAlongLine(
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

        private void FindPointNeighbourPairs(
            List<Line<float>> lines,
            List<Intersection> intersections,
            List<Point<float>> points,
            List<LineSegment<float>> connections)
        {
            foreach (var line in lines)
            {
                var pointsOnLine = FindPointsAlongLine(line, intersections);

                var previous = new Point<float>(float.NaN, float.NaN);

                foreach (var point in pointsOnLine)
                {
                    points.Add(point);

                    if (!float.IsNaN(previous.X))
                    {
                        connections.Add(new LineSegment<float>()
                        {
                            A = previous,
                            B = point
                        });
                    }

                    previous = point;
                }
            }
        }

        public void GenerateLevel(int lineCount)
        {
            var lines = new List<Line<float>>();
            var intersections = new List<Intersection>();
            var points = new List<Point<float>>();
            var connections = new List<LineSegment<float>>();

            GenerateIntersections(lineCount, lines, intersections);
            FindPointNeighbourPairs(lines, intersections, points, connections);

            Points = points;
            Connections = connections;
        }
    }
}
