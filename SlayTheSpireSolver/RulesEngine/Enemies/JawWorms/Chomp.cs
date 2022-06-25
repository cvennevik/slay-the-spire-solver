﻿using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Chomp : IJawWormMove
{
    private static readonly Damage BaseDamage = new(12);

    public EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack(new AttackPlayerEffect(enemy.Id, BaseDamage));
    }
}
