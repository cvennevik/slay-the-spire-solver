using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Chomp : EnemyMove
{
    private static readonly Damage BaseDamage = new(11);

    public override EffectStack GetEffects(Enemy enemy) => new AttackPlayerEffect(enemy.Id, BaseDamage);

    protected override string GetName() => "Chomp";
}
