using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvablePossibilitySet : IEnumerable<Possibility>, IEquatable<ResolvablePossibilitySet>
{
    private readonly List<Possibility> _possibilities;

    public ResolvablePossibilitySet(params Possibility[] possibilities)
    {
        _possibilities = possibilities.Distinct().ToList();
    }

    public static implicit operator ResolvablePossibilitySet(GameState gameState) =>
        new(gameState.WithEffects());

    public static implicit operator ResolvablePossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => (Possibility)x.WithEffects()).ToArray());

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);

    public static implicit operator ResolvablePossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates.Select(x => (Possibility)x).ToArray());

    public static implicit operator ResolvablePossibilitySet(
        Possibility resolvableGameState) => new(resolvableGameState);

    public static implicit operator ResolvablePossibilitySet(
        Possibility[] resolvableGameStates) => new(resolvableGameStates);

    public IEnumerator<Possibility> GetEnumerator()
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