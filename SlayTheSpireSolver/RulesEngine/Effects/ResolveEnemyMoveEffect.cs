using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : TargetEnemyEffect
{
    public ResolveEnemyMoveEffect() : base() { }
    public ResolveEnemyMoveEffect(EnemyId Target) : base(Target) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var enemyMoveEffects = gameState.EnemyParty.Get(Target).GetMoveEffects();
        return gameState.WithEffects(enemyMoveEffects);
    }

    public void Deconstruct(out EnemyId Target)
    {
        Target = this.Target;
    }
}