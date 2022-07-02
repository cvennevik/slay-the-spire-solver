using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ChooseNewEnemyMoveEffect(EnemyId Target) : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}