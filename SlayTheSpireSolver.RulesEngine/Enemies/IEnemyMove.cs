﻿using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record EnemyMove
{
    public abstract EffectStack GetEffects(Enemy enemy);
    protected abstract string GetName();
    public sealed override string ToString() => GetName();
}
