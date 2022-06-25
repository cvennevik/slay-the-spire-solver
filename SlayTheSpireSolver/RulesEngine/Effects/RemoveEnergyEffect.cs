using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct RemoveEnergyEffect : IEffect
{
    private readonly Energy _energyToRemove;

    public RemoveEnergyEffect(Energy energyToRemove)
    {
        _energyToRemove = energyToRemove;
    }

    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        return new[] { new ResolvableGameState(gameState with { Energy = gameState.Energy - _energyToRemove }) };
    }
}