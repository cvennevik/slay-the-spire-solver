using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : TargetEnemyEffect
{
    public ResolveEnemyMoveEffect() { }
    public ResolveEnemyMoveEffect(EnemyId target) : base(target) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var enemyMoveEffects = gameState.EnemyParty.Get(Target).GetMoveEffects();
        return gameState.WithEffects(enemyMoveEffects);
    }
}