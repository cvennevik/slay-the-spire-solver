using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStatePossibilitySet : IEnumerable<ResolvableGameState>, IEquatable<ResolvableGameStatePossibilitySet>
{
    private readonly List<ResolvableGameState> _possibilities;

    public ResolvableGameStatePossibilitySet(ResolvableGameState[] resolvableGameStates)
    {
        _possibilities = new List<ResolvableGameState>(resolvableGameStates.Distinct());
    }

    public ResolvableGameStatePossibilitySet(params ResolvableGameStatePossibility[] possibilities)
    {
        _possibilities = possibilities.Select(x => x.ResolvableGameState).Distinct().ToList();
    }

    public static implicit operator ResolvableGameStatePossibilitySet(GameState gameState) => new(gameState.WithEffects());
    public static implicit operator ResolvableGameStatePossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => (ResolvableGameStatePossibility) x.WithEffects()).ToArray());
    public static implicit operator ResolvableGameStatePossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);
    public static implicit operator ResolvableGameStatePossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates.Select(x => (ResolvableGameStatePossibility)x).ToArray());

    public IEnumerator<ResolvableGameState> GetEnumerator()
    {
        return _possibilities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(ResolvableGameStatePossibilitySet? other)
    {
        if (other == null) return false;
        return _possibilities.Count == other._possibilities.Count &&
               _possibilities.All(other.Contains);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ResolvableGameStatePossibilitySet);
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