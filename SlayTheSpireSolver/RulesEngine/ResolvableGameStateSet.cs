using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStateSet : IEnumerable<ResolvableGameState>, IEquatable<ResolvableGameStateSet>
{
    private readonly List<ResolvableGameState> _resolvableGameStates;

    public static implicit operator ResolvableGameStateSet(GameState gameState) => new(gameState.AsResolvable());
    public static implicit operator ResolvableGameStateSet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);
    public static implicit operator ResolvableGameStateSet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates);

    public ResolvableGameStateSet(params ResolvableGameState[] resolvableGameStates)
    {
        _resolvableGameStates = new List<ResolvableGameState>(resolvableGameStates);
    }

    public IEnumerator<ResolvableGameState> GetEnumerator()
    {
        return _resolvableGameStates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ResolvableGameStateSet otherSet) return false;
        return _resolvableGameStates.Count == otherSet._resolvableGameStates.Count &&
               _resolvableGameStates.All(x => otherSet.Contains(x));
    }

    bool IEquatable<ResolvableGameStateSet>.Equals(ResolvableGameStateSet? other)
    {
        if (other == null) return false;
        return _resolvableGameStates.Count == other._resolvableGameStates.Count &&
               _resolvableGameStates.All(other.Contains);
    }

    public override int GetHashCode()
    {
        return _resolvableGameStates.GetHashCode();
    }
}