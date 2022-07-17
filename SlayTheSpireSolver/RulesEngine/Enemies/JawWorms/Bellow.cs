using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Bellow : IEnemyMove
{
    private static readonly Strength StrengthGain = 3;
    private static readonly Armor ArmorGain = 6;

    public EffectStack GetEffects(Enemy enemy)
    {
        return new Effect[]
        {
            new AddEnemyArmorEffect(enemy.Id, ArmorGain),
            new AddEnemyStrengthEffect(enemy.Id, StrengthGain)
        };
    }
}
