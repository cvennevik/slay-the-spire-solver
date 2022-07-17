using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Thrash : EnemyMove
{
    private static readonly Damage BaseDamage = 7;
    private static readonly Armor ArmorGain = 5;

    public override EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack(new AddEnemyArmorEffect(enemy.Id, ArmorGain),
            new AttackPlayerEffect(enemy.Id, BaseDamage));
    }
}
