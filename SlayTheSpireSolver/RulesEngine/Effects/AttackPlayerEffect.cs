namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState;
    }
}