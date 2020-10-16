using System;
using NUnit.Framework;
using BushidoBurrito.Planarity;

namespace BushidoBurrito.PlanarityTest
{
    [TestFixture()]
    public class Geometry2dTest
    {
        [TestCase(0, 0, 1, 0, Result = 0)]
        [TestCase(0, 0, 1, 1, Result = 1)]
        [TestCase(1, 1, 0, 0, Result = 1)]
        [TestCase(1, 1, 2, 2, Result = 1)]
        [TestCase(-1, -1, -2, -2, Result = 1)]
        [TestCase(0, 0, 1, 3, Result = 3)]
        [TestCase(0, 0, -1, 3, Result = -3)]
        [TestCase(1, 1, 2, -2, Result = -3)]
        [TestCase(0, 0, 0, 1, Result = float.NaN)]
        [TestCase(1, 1, 1, 0, Result = float.NaN)]
        public float TestSlope(float a_x, float a_y, float b_x, float b_y)
        {
            return Geometry2d.Slope(a_x, a_y, b_x, b_y);
        }

        [TestCase(0, 0, 0, 0, 0, 0, Result = false)]
        [TestCase(0, 0, 0, 0, 1, 1, Result = false)]
        [TestCase(0, 0, 1, 1, 0, 0, Result = false)]
        [TestCase(1, 1, 1, 0, 0, 0, Result = true)]
        [TestCase(0, 0, 0, 0, 1, -1, Result = false)]
        [TestCase(0, 0, 1, -1, 0, 0, Result = false)]
        [TestCase(0, 0, 1, 0, 1, -1, Result = true)]
        [TestCase(0, 0, 1, 0, 1, 1, Result = false)]
        [TestCase(5, 5, 6, 4, 5, 3, Result = true)]
        [TestCase(5, 5, 6, 4, 7, 3, Result = false)]
        public bool TestIsClockwise(float a_x, float a_y, float b_x, float b_y, float c_x, float c_y)
        {
            return Geometry2d.IsClockwise(a_x, a_y, b_x, b_y, c_x, c_y);
        }

        [TestCase(1, -1, 0, 1, Result = 0.5)]
        [TestCase(0, 0, 1, 0, Result = float.NaN)]
        public float TestIntersectionX(float a_y_intercept, float a_slope, float b_y_intercept, float b_slope)
        {
            var a = new Line<float>
            {
                YIntercept = a_y_intercept,
                Slope = a_slope
            };

            var b = new Line<float>
            {
                YIntercept = b_y_intercept,
                Slope = b_slope
            };

            return Geometry2d.IntersectionX(a, b);
        }
    }
}
