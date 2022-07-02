namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearPlayerArmorEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with {PlayerArmor = 0};
    }
}