using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStatePossibilitySet : IEnumerable<ResolvableGameState>, IEquatable<ResolvableGameStatePossibilitySet>
{
    private readonly List<ResolvableGameState> _resolvableGameStates;

    public static implicit operator ResolvableGameStatePossibilitySet(GameState gameState) => new(gameState.WithEffects());

    public static implicit operator ResolvableGameStatePossibilitySet(GameState[] gameStates) =>
        new(gameStates.Select(x => x.WithEffects()).ToArray());
    public static implicit operator ResolvableGameStatePossibilitySet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);
    public static implicit operator ResolvableGameStatePossibilitySet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates);

    public ResolvableGameStatePossibilitySet(params ResolvableGameState[] resolvableGameStates)
    {
        _resolvableGameStates = new List<ResolvableGameState>(resolvableGameStates.Distinct());
    }

    public IEnumerator<ResolvableGameState> GetEnumerator()
    {
        return _resolvableGameStates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(ResolvableGameStatePossibilitySet? other)
    {
        if (other == null) return false;
        return _resolvableGameStates.Count == other._resolvableGameStates.Count &&
               _resolvableGameStates.All(other.Contains);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ResolvableGameStatePossibilitySet);
    }

    public override int GetHashCode()
    {
        return _resolvableGameStates.GetHashCode();
    }
}