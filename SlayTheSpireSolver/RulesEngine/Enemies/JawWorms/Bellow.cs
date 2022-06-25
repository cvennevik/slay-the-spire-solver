using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Bellow : IJawWormMove
{
    public EffectStack GetEffects(Enemy enemy)
    {
        throw new NotImplementedException();
    }
}