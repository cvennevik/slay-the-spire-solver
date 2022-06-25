namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}