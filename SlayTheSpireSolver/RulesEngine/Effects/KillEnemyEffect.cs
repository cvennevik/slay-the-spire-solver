using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId TargetId) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { EnemyParty = gameState.EnemyParty.Remove(TargetId) };
    }
}