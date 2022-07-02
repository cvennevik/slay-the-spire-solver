using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveEnergyEffect : Effect
{
    private readonly Energy _energyToRemove;

    public RemoveEnergyEffect(Energy energyToRemove)
    {
        _energyToRemove = energyToRemove;
    }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.Energy - _energyToRemove };
    }
}