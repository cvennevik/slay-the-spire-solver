namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public virtual ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        return gameState;
    }

    public virtual PossibilitySet NewResolve(GameState gameState)
    {
        var originalEffectStack = gameState.EffectStack;
        var resolvablePossibilities = OldResolve(gameState with { EffectStack = new EffectStack() }).ToList();
        var possibilitiesWithoutOriginalEffectStack = resolvablePossibilities.Select(x =>
            new Possibility(x.GameState with { EffectStack = x.GameState.EffectStack }, x.Probability));
        var possibilities = possibilitiesWithoutOriginalEffectStack.Select(x =>
            x with { GameState = x.GameState with { EffectStack = originalEffectStack.Push(x.GameState.EffectStack) } });
        return possibilities.ToArray();
    }
}