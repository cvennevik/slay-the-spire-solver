using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect(EnemyId Target) : TargetEnemyEffect(Target)
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var enemyMoveEffects = gameState.EnemyParty.Get(Target).GetMoveEffects();
        return gameState.WithEffects(enemyMoveEffects);
    }
}