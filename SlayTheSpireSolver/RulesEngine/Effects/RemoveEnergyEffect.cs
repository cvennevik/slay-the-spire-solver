using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveEnergyEffect(Energy _energyToRemove) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.Energy - _energyToRemove };
    }
}