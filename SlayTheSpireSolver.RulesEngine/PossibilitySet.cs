using System.Collections;
using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine;

public class PossibilitySet : IEnumerable<Possibility>, IEquatable<PossibilitySet>
{
    private readonly List<Possibility> _possibilities;

    public PossibilitySet(params Possibility[] possibilities)
    {
        _possibilities = possibilities.ToList();
    }

    public PossibilitySet(List<Possibility> possibilities)
    {
        _possibilities = possibilities;
    }

    public static implicit operator PossibilitySet(GameState gameState)
    {
        return new PossibilitySet(gameState.WithProbability(1));
    }

    public static implicit operator PossibilitySet(Possibility[] possibilities)
    {
        return new PossibilitySet(possibilities);
    }

    public static implicit operator PossibilitySet(List<Possibility> possibilities)
    {
        return new PossibilitySet(possibilities);
    }

    public IEnumerator<Possibility> GetEnumerator()
    {
        return _possibilities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(PossibilitySet? other)
    {
        if (other == null) return false;
        return _possibilities.Count == other._possibilities.Count &&
               _possibilities.All(other.Contains);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PossibilitySet);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(",\n", _possibilities)}]";
    }
}

[TestFixture]
internal class PossibilitySetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new PossibilitySet(), new PossibilitySet());
    }
}