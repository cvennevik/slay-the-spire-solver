namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearPlayerArmorEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with {PlayerArmor = 0};
    }
}