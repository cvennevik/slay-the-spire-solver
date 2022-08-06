using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine;

public class RelicCollection
{
    private readonly Relic[] _relics;
    private readonly int _hashCode;

    public RelicCollection(params Relic[] relics)
    {
        Array.Sort(relics);
        _relics = relics;
        _hashCode = _relics.Aggregate(0, HashCode.Combine);
    }

    public bool Contains(Relic relic)
    {
        return _relics.Contains(relic);
    }

    public static bool operator ==(RelicCollection a, RelicCollection b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(RelicCollection a, RelicCollection b)
    {
        return !a.Equals(b);
    }

    public override bool Equals(object? obj)
    {
        return obj is RelicCollection otherRelicCollection && Equals(otherRelicCollection);
    }

    private bool Equals(RelicCollection other)
    {
        return ReferenceEquals(this, other) || _relics.SequenceEqual(other._relics);
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override string ToString()
    {
        return $"[{string.Join<Relic>(",", _relics)}]";
    }
}

[TestFixture]
internal class RelicCollectionTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RelicCollection(), new RelicCollection());
        Assert.AreEqual(new RelicCollection(new BurningBlood()), new RelicCollection(new BurningBlood()));
        Assert.AreNotEqual(new RelicCollection(new BurningBlood()), new RelicCollection());
        Assert.AreEqual(new RelicCollection(new BurningBlood(), new OldCoin()),
            new RelicCollection(new OldCoin(), new BurningBlood()));
    }

    [Test]
    public void TestHashCodeEquality()
    {
        Assert.AreEqual(new RelicCollection().GetHashCode(), new RelicCollection().GetHashCode());
        Assert.AreEqual(new RelicCollection(new BurningBlood()).GetHashCode(),
            new RelicCollection(new BurningBlood()).GetHashCode());
        Assert.AreEqual(new RelicCollection(new BurningBlood(), new OldCoin()).GetHashCode(),
            new RelicCollection(new OldCoin(), new BurningBlood()).GetHashCode());
    }

    [Test]
    public void TestContains()
    {
        Assert.False(new RelicCollection().Contains(new BurningBlood()));

        Assert.True(new RelicCollection(new BurningBlood()).Contains(new BurningBlood()));
        Assert.False(new RelicCollection(new BurningBlood()).Contains(new OldCoin()));

        Assert.True(new RelicCollection(new BurningBlood(), new OldCoin()).Contains(new BurningBlood()));
        Assert.True(new RelicCollection(new BurningBlood(), new OldCoin()).Contains(new OldCoin()));
    }
}