namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}