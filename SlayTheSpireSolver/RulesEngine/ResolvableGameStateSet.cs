using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStateSet : IEnumerable<ResolvableGameState>, IEquatable<ResolvableGameStateSet>
{
    private readonly List<ResolvableGameState> _resolvableGameStates;

    public static implicit operator ResolvableGameStateSet(GameState gameState) => new(gameState.WithEffects());

    public static implicit operator ResolvableGameStateSet(GameState[] gameStates) =>
        new(gameStates.Select(x => x.WithEffects()).ToArray());
    public static implicit operator ResolvableGameStateSet(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState);
    public static implicit operator ResolvableGameStateSet(ResolvableGameState[] resolvableGameStates) =>
        new(resolvableGameStates);

    public ResolvableGameStateSet(params ResolvableGameState[] resolvableGameStates)
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