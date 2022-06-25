using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct RemoveEnergyEffect : IEffect
{
    private readonly Energy _energyToRemove;

    public RemoveEnergyEffect(Energy energyToRemove)
    {
        _energyToRemove = energyToRemove;
    }

    public IReadOnlyCollection<UnresolvedGameState> Apply(GameState gameState)
    {
        return new[] { new UnresolvedGameState(gameState with { Energy = gameState.Energy - _energyToRemove }) };
    }
}