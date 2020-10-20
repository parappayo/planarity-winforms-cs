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

    public class GameState
    {
        public readonly List<Pip> Pips;
        public readonly List<Edge<Pip>> Connections;

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

        public void CheckWinCondition()
        {
            throw new NotImplementedException();
        }
    }
}
