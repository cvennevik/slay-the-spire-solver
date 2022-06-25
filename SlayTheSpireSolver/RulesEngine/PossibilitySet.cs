using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class PossibilitySet : IEnumerable<ResolvableGameStatePossibility>, IEquatable<PossibilitySet>
{
    private readonly List<ResolvableGameStatePossibility> _possibilities;

    public PossibilitySet(params ResolvableGameStatePossibility[] possibilities)
    {
        _possibilities = possibilities.Distinct().ToList();
    }

    public static implicit operator PossibilitySet(GameState gameState) =>
        new(gameState.WithEffects());

    public static implicit operator PossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => (ResolvableGameStatePossibility)x.WithEffects()).ToArray());

    public static implicit operator PossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);

    public static implicit operator PossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates.Select(x => (ResolvableGameStatePossibility)x).ToArray());

    public static implicit operator PossibilitySet(
        ResolvableGameStatePossibility resolvableGameState) => new(resolvableGameState);

    public static implicit operator PossibilitySet(
        ResolvableGameStatePossibility[] resolvableGameStates) => new(resolvableGameStates);

    public IEnumerator<ResolvableGameStatePossibility> GetEnumerator()
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
        return _possibilities.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{string.Join(",\n", _possibilities)}]";
    }
}