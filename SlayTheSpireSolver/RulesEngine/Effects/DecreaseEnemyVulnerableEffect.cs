using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DecreaseEnemyVulnerableEffect : TargetEnemyEffect
{
    public DecreaseEnemyVulnerableEffect() { }
    public DecreaseEnemyVulnerableEffect(EnemyId enemyId) : base(enemyId) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}