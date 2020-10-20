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
            Set(p);
        }

        public void Set(Point<float> p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point<float> Point
        {
            get { return new Point<float>(X, Y); }
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

    public struct Collision
    {
        public Edge<Pip> Connection1;
        public Edge<Pip> Connection2;

        public bool Contains(Edge<Pip> connection)
        {
            if (Connection1 == null || Connection2 == null)
            {
                return false;
            }

            return Connection1.Equals(connection) ||
                Connection2.Equals(connection);
        }
    }

    public class GameState
    {
        public readonly List<Pip> Pips;
        public readonly List<Edge<Pip>> Connections;

        public bool LevelComplete { get; private set; }

        public Collision LastFoundCollision { get; private set; }

        private void ArrangeInCircle(List<Pip> pips)
        {
            var center = new Point<float>(1024 / 2, 768 / 2);
            float radius = 768 / 3;
            var circlePoints = Geometry2d.CirclePoints(center, radius, pips.Count);

            int i = 0;
            foreach (var pip in pips)
            {
                pip.Set(circlePoints[i]);
                i += 1;
            }
        }

        public GameState()
        {
            var levelGenerator = new LevelGenerator();
            levelGenerator.GenerateLevel(5);

            Pips = levelGenerator.Pips;
            Connections = levelGenerator.Connections;

            ArrangeInCircle(Pips);
        }

        public Pip FindPip(Point<float> position, float radius)
        {
            float radiusSquared = radius * radius;

            foreach (var pip in Pips)
            {
                float dx = pip.X - position.X;
                float dy = pip.Y - position.Y;

                if (dx * dx + dy * dy < radiusSquared)
                {
                    return pip;
                }
            }

            return null;
        }

        private LineSegment<float> ToLineSegment(Edge<Pip> connection)
        {
            return new LineSegment<float>()
            {
                A = connection.From.Point,
                B = connection.To.Point
            };
        }

        private bool Intersects(Edge<Pip> connection1, Edge<Pip> connection2)
        {
            return Geometry2d.Intersects(
                ToLineSegment(connection1),
                ToLineSegment(connection2));
        }

        public void CheckWinCondition()
        {
            if (LevelComplete)
            {
                return;
            }

            foreach (var connection1 in Connections)
            {
                foreach (var connection2 in Connections)
                {
                    if (connection1.Equals(connection2))
                    {
                        continue;
                    }

                    if (Intersects(connection1, connection2))
                    {
                        LastFoundCollision = new Collision()
                        {
                            Connection1 = connection1,
                            Connection2 = connection2
                        };
                        return;
                    }
                }
            }

            LastFoundCollision = new Collision();
            LevelComplete = true;
        }
    }
}
