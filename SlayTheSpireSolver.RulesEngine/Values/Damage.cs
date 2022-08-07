using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Buffs;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct Damage
{
    public int Amount { get; }

    public Damage(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public Damage AgainstVulnerableEnemy()
    {
        return new Damage((int)(Amount * 1.5));
    }

    public static bool operator >(Damage damage, Armor armor)
    {
        return damage.Amount > armor.Amount;
    }

    public static bool operator >=(Damage damage, Armor armor)
    {
        return damage.Amount >= armor.Amount;
    }

    public static bool operator <(Damage damage, Armor armor)
    {
        return damage.Amount < armor.Amount;
    }

    public static bool operator <=(Damage damage, Armor armor)
    {
        return damage.Amount <= armor.Amount;
    }

    public static Damage operator -(Damage damage, Armor armor)
    {
        return new Damage(damage < armor ? 0 : damage.Amount - armor.Amount);
    }

    public static Damage operator +(Damage damage, Strength strength)
    {
        return new Damage(damage.Amount + strength.Amount);
    }

    public static implicit operator Damage(int amount)
    {
        return amount > 0 ? new Damage(amount) : new Damage(0);
    }

    public override string ToString()
    {
        return $"{Amount}";
    }
}

[TestFixture]
internal class DamageTests
{
    [TestFixture]
    internal class BasicTests : DamageTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-999)]
        public void AmountCannotBeNegative(int amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Damage(amount));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void DamageWithEqualAmountsAreEqual(int amount)
        {
            Assert.AreEqual(new Damage(amount), new Damage(amount));
        }
    }

    [TestFixture]
    internal class ArmorOperatorTests : DamageTests
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
        public void TestSubtraction(int amountOfDamage, int amountOfArmor, int remainingAmountOfDamage)
        {
            Assert.AreEqual(new Damage(remainingAmountOfDamage),
                new Damage(amountOfDamage) - new Armor(amountOfArmor));
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
        public void TestGreaterThan(int amountOfDamage, int amountOfArmor, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Damage(amountOfDamage) > new Armor(amountOfArmor));
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
        public void TestGreaterThanOrEqualTo(int amountOfDamage, int amountOfArmor, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Damage(amountOfDamage) >= new Armor(amountOfArmor));
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
        public void TestLessThan(int amountOfDamage, int amountOfArmor, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Damage(amountOfDamage) < new Armor(amountOfArmor));
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
        public void TestLessThanOrEqualTo(int amountOfDamage, int amountOfArmor, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, new Damage(amountOfDamage) <= new Armor(amountOfArmor));
        }
    }

    [TestFixture]
    internal class ImplicitConversionTests : DamageTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void ImplicitlyConvertedDamageEqualsExplicitDamage(int amount)
        {
            Damage damage = amount;
            Assert.AreEqual(new Damage(amount), damage);
            Assert.True(new Damage(amount) == amount);
            Assert.True(amount == new Damage(amount));
        }
    }
}