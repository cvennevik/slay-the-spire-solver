using System.Collections;
using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvablePossibilitySet : IEnumerable<ResolvablePossibility>, IEquatable<ResolvablePossibilitySet>
{
    private readonly List<ResolvablePossibility> _possibilities;

    public ResolvablePossibilitySet(params ResolvablePossibility[] possibilities)
    {
        _possibilities = possibilities.Distinct().ToList();
    }

    public static implicit operator ResolvablePossibilitySet(GameState gameState) =>
        new(gameState.WithEffects().WithProbability(1));

    public static implicit operator ResolvablePossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => x.WithEffects().WithProbability(1)).ToArray());

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState.WithProbability(1));

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates.Select(x => x.WithProbability(1)).ToArray());

    public static implicit operator ResolvablePossibilitySet(
        ResolvablePossibility resolvableGameState) => new(resolvableGameState);

    public static implicit operator ResolvablePossibilitySet(
        ResolvablePossibility[] resolvableGameStates) => new(resolvableGameStates);

    public static implicit operator ResolvablePossibilitySet(Possibility possibility) => new(possibility);

    public static implicit operator ResolvablePossibilitySet(Possibility[] possibilities) =>
        new(possibilities.Select(x => x.GameState.WithEffects().WithProbability(x.Probability)).ToArray());

    public IEnumerator<ResolvablePossibility> GetEnumerator()
    {
        return _possibilities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(ResolvablePossibilitySet? other)
    {
        if (other == null) return false;
        return _possibilities.Count == other._possibilities.Count &&
               _possibilities.All(other.Contains);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ResolvablePossibilitySet);
    }

    public override int GetHashCode()
    {
        return _possibilities.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{string.Join(",\n", _possibilities)}]";
    }
}

[TestFixture]
internal class ResolvablePossibilitySetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(), new ResolvablePossibilitySet());
    }

    [Test]
    public void TestSingleEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState()), new ResolvablePossibilitySet(new GameState()));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 3 }),
            new ResolvablePossibilitySet(new GameState { Turn = 3 }));
        Assert.AreNotEqual(new ResolvablePossibilitySet(new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 3 }));
    }

    [Test]
    public void TestMultipleEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 2 }, new GameState { Turn = 1 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
    }

    [Test]
    public void TestDuplicateEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 }));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 },
                new GameState { Turn = 2 }));
    }
}