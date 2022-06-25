using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Bellow : IJawWormMove
{
    private static readonly Strength StrengthGain = 5;
    private static readonly Armor ArmorGain = 9; 

    public EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack();
    }
}