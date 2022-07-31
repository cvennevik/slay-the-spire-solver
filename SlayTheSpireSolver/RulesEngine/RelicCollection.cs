using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine;

public class RelicCollection
{
    private readonly Relic[] _relics;

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
        if (ReferenceEquals(this, obj)) return true;
        return obj is RelicCollection otherRelicCollection && _relics.SequenceEqual(otherRelicCollection._relics);
    }

    private bool Equals(RelicCollection other)
    {
        if (ReferenceEquals(this, other)) return true;
        return _relics.SequenceEqual(other._relics);
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
    }
}