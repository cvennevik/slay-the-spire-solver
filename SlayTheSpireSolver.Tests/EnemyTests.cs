using NUnit.Framework;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class EnemyTests
{
    public class EqualityTests : EnemyTests
    {
        [Test]
        public void Test1()
        {
            var enemy1 = new Enemy();
            var enemy2 = new Enemy();
            Assert.AreEqual(enemy1, enemy2);
        }

        [Test]
        public void Test2()
        {
            var enemy1 = new Enemy { Health = 1 };
            var enemy2 = new Enemy { Health = 1 };
            Assert.AreEqual(enemy1, enemy2);
        }

        [Test]
        public void Test3()
        {
            var enemy1 = new Enemy { Health = 1 };
            var enemy2 = new Enemy { Health = 2 };
            Assert.AreNotEqual(enemy1, enemy2);
        }
    }
}
