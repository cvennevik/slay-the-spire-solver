namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState;
    }
}