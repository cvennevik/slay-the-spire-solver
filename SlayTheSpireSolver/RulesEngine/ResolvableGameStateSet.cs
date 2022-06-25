using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStateSet : IEnumerable<ResolvableGameState>
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
        if (_resolvableGameStates.Count != otherSet._resolvableGameStates.Count) return false;
        return _resolvableGameStates.All(x => otherSet.Contains(x));
    }

    public override int GetHashCode()
    {
        return 0;
    }
}