using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public interface IJawWormMove : IEnemyMove
{
    EffectStack GetEffects(Enemy enemy);
}
