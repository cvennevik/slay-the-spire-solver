using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public interface IEnemyMove
{
    EffectStack GetEffects(Enemy enemy);
}
