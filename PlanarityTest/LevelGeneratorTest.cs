using NUnit.Framework;
using BushidoBurrito.Planarity;

namespace BushidoBurrito.PlanarityTest
{
    [TestFixture()]
    public class LevelGeneratorTest
    {
        [Test()]
        public void TestLevelGenerator()
        {
            var levelGenerator = new LevelGenerator();
            levelGenerator.GenerateLevel(5);

            Assert.IsTrue(levelGenerator.Connections.Count > 0);
            Assert.IsTrue(levelGenerator.Points.Count > 0);
        }
    }
}
