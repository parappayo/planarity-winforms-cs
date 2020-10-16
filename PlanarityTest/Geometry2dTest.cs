using NUnit.Framework;
using BushidoBurrito.Planarity;

namespace BushidoBurrito.PlanarityTest
{
    [TestFixture()]
    public class Geometry2dTest
    {
        [TestCase(0F, 0F, 0.01F, Result = true)]
        [TestCase(1F, 2F, 0.01F, Result = false)]
        [TestCase(float.PositiveInfinity, float.NegativeInfinity, 0.01F, Result = false)]
        [TestCase(float.NaN, float.NaN, 0.01F, Result = true)]
        public bool TestNearlyEqual(float a, float b, float epsilon)
        {
            return Geometry2d.NearlyEqual(a, b, epsilon);
        }

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
            var a = new Point<float>(a_x, a_y);
            var b = new Point<float>(b_x, b_y);

            return Geometry2d.Slope(a, b);
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
            var a = new Point<float>(a_x, a_y);
            var b = new Point<float>(b_x, b_y);
            var c = new Point<float>(c_x, c_y);

            return Geometry2d.IsClockwise(a, b, c);
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

        [Test()]
        public void TestIntersectionPoint()
        {
            var lineA = new Line<float>()
            {
                YIntercept = -1,
                Slope = 1
            };

            var lineB = new Line<float>()
            {
                YIntercept = 1,
                Slope = -1
            };

            var result = Geometry2d.IntersectionPoint(lineA, lineB);

            Assert.AreEqual(result.X, 1F);
            Assert.AreEqual(result.Y, 0F);
        }

        [Test()]
        public void TestSharesPoint()
        {
            var lineA = new LineSegment<float>
            {
                A = new Point<float>(0, 0),
                B = new Point<float>(1, 1)
            };

            var lineB = new LineSegment<float>
            {
                A = new Point<float>(1, 1),
                B = new Point<float>(2, 2)
            };

            var lineC = new LineSegment<float>
            {
                A = new Point<float>(2, 2),
                B = new Point<float>(3, 3)
            };

            Assert.IsTrue(Geometry2d.SharesPoint(lineA, lineB));
            Assert.IsFalse(Geometry2d.SharesPoint(lineA, lineC));
        }

        [Test()]
        public void TestIntersects()
        {
            var lineA = new LineSegment<float>
            {
                A = new Point<float>(0, -1),
                B = new Point<float>(2, 1)
            };

            var lineB = new LineSegment<float>
            {
                A = new Point<float>(0, 1),
                B = new Point<float>(2, -1)
            };

            var lineC = new LineSegment<float>
            {
                A = new Point<float>(0F, 0F),
                B = new Point<float>(0.5F, 2F)
            };

            Assert.IsTrue(Geometry2d.Intersects(lineA, lineB));
            Assert.IsTrue(Geometry2d.Intersects(lineB, lineC));
            Assert.IsFalse(Geometry2d.Intersects(lineA, lineC));
        }
    }
}
