using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Chomp : IEnemyMove
{
    private static readonly Damage BaseDamage = new(11);

    public EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack(new AttackPlayerEffect(enemy.Id, BaseDamage));
    }
}
