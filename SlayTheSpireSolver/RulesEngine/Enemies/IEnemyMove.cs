using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public interface IEnemyMove
{
    GameState Resolve(GameState gameState);
    EffectStack GetEffects(Enemy enemy);
}
