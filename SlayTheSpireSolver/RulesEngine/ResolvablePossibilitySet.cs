using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvablePossibilitySet : IEnumerable<ResolvablePossibility>, IEquatable<ResolvablePossibilitySet>
{
    private readonly List<ResolvablePossibility> _possibilities;

    public ResolvablePossibilitySet(params ResolvablePossibility[] possibilities)
    {
        _possibilities = possibilities.Distinct().ToList();
    }

    public static implicit operator ResolvablePossibilitySet(GameState gameState) =>
        new(gameState.WithEffects());

    public static implicit operator ResolvablePossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => (ResolvablePossibility)x.WithEffects()).ToArray());

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates.Select(x => (ResolvablePossibility)x).ToArray());

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