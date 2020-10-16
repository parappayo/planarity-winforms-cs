using System.Drawing;
using NUnit.Framework;
using BushidoBurrito.Planarity;

namespace BushidoBurrito.PlanarityTest
{
    [TestFixture()]
    public class Geometry2dTest
    {
        [Test()]
        public void TestSlope()
        {
            var slope = Geometry2d.Slope(new Point(0, 0), new Point(1, 1));

            Assert.AreEqual(slope, 1);
        }
    }
}
