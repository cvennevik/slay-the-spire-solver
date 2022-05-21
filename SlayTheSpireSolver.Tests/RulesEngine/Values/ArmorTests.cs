using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Values;

[TestFixture]
public class ArmorTests
{
    [TestFixture]
    public class SelfTests : ArmorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-999)]
        public void AmmountCannotBeNegative(int amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Armor(amount));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void ArmorWithEqualAmountsAreEqual(int amount)
        {
            Assert.AreEqual(new Armor(amount), new Armor(amount));
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 1, 3)]
        [TestCase(2, 2, 4)]
        public void TestPlus(int amountA, int amountB, int expectedAmount)
        {
            Assert.AreEqual(new Armor(expectedAmount), new Armor(amountA) + new Armor(amountB));
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 0)]
        public void TestMinus(int amountA, int amountB, int expectedAmount)
        {
            Assert.AreEqual(new Armor(expectedAmount), new Armor(amountA) - new Armor(amountB));
        }
    }

    [TestFixture]
    public class DamageOperatorTests : ArmorTests
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 0)]
        public void TestSubtraction(int amountOfArmor, int amountOfDamage, int remainingAmountOfArmor)
        {
            Assert.AreEqual(new Armor(remainingAmountOfArmor),
                new Armor(amountOfArmor) - new Damage(amountOfDamage));
        }

        [Test]
        [TestCase(0, 0, false)]
        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(1, 0, true)]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, false)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        public void TestGreaterThan(int amountOfArmor, int amountOfDamage, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Armor(amountOfArmor) > new Damage(amountOfDamage));
        }
    
        [Test]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(1, 0, true)]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, true)]
        public void TestGreaterThanOrEqualTo(int amountOfArmor, int amountOfDamage, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Armor(amountOfArmor) >= new Damage(amountOfDamage));
        }
    
        [Test]
        [TestCase(0, 0, false)]
        [TestCase(0, 1, true)]
        [TestCase(0, 2, true)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        [TestCase(2, 0, false)]
        [TestCase(2, 1, false)]
        [TestCase(2, 2, false)]
        public void TestLessThan(int amountOfArmor, int amountOfDamage, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Armor(amountOfArmor) < new Damage(amountOfDamage));
        }
    
        [Test]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, true)]
        [TestCase(0, 2, true)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, true)]
        [TestCase(2, 0, false)]
        [TestCase(2, 1, false)]
        [TestCase(2, 2, true)]
        public void TestLessThanOrEqualTo(int amountOfArmor, int amountOfDamage, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Armor(amountOfArmor) <= new Damage(amountOfDamage));
        }
    }
}