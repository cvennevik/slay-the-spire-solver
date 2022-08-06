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
        return _relics.GetHashCode();
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
}