using System.Collections.Generic;
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
            Assert.IsTrue(levelGenerator.Pips.Count > 0);

            var edgePips = Edge<Pip>.Flatten(levelGenerator.Connections);
            foreach (var pip in levelGenerator.Pips)
            {
                Assert.Contains(pip, edgePips);
            }
        }
    }
}
