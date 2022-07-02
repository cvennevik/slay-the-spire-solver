namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}