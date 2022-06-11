using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct RemoveEnergyEffect : IEffect
{
    private readonly Energy _energyToRemove;

    public RemoveEnergyEffect(Energy energyToRemove)
    {
        _energyToRemove = energyToRemove;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        return new[] { gameState with { Energy = gameState.Energy - _energyToRemove } };
    }

    public IReadOnlyCollection<GameStateWithUnresolvedEffects> Resolve(GameState gameState)
    {
        var result = ApplyTo(gameState);
        return result.Select(x => new GameStateWithUnresolvedEffects(x)).ToList();
    }
}