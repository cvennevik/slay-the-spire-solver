namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);

    public virtual IReadOnlyCollection<Possibility> NewResolve(GameState gameState)
    {
        var originalEffectStack = gameState.EffectStack;
        var resolvablePossibilities = Resolve(gameState with { EffectStack = new EffectStack() }).ToList();
        var possibilitiesWithoutOriginalEffectStack = resolvablePossibilities.Select(x =>
            new Possibility(x.GameState with { EffectStack = x.GameState.EffectStack }, x.Probability));
        var possibilities = possibilitiesWithoutOriginalEffectStack.Select(x =>
            x with { GameState = x.GameState with { EffectStack = originalEffectStack.Push(x.GameState.EffectStack) } });
        return possibilities.ToList();
    }
}