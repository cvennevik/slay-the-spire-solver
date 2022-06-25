namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}