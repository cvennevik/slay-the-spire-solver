using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveEnergyEffect : IEffect
{
    private readonly Energy _energyToRemove;

    public RemoveEnergyEffect(Energy energyToRemove)
    {
        _energyToRemove = energyToRemove;
    }

    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.Energy - _energyToRemove };
    }
}