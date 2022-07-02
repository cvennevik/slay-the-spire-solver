using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DecreaseEnemyVulnerableEffect : TargetEnemyEffect
{
    public DecreaseEnemyVulnerableEffect() { }
    public DecreaseEnemyVulnerableEffect(EnemyId enemyId) : base(enemyId) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;

        return gameState.ModifyEnemy(Target, enemy => enemy with { Vulnerable = enemy.Vulnerable.Duration - 1 });
    }
}