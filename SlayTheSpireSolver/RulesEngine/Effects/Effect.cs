namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);

    public virtual IReadOnlyCollection<Possibility> NewResolve(GameState gameState)
    {
        var originalEffectStack = gameState.EffectStack;
        var resolvablePossibilities = Resolve(gameState with { EffectStack = new EffectStack() });
        return Resolve(gameState with {EffectStack = new EffectStack()})
            .Select(resolvablePossibility =>
                new Possibility(
                    resolvablePossibility.GameState with
                    {
                        EffectStack = originalEffectStack.Push(resolvablePossibility.GameState.EffectStack)
                    }, resolvablePossibility.Probability))
            .ToList();
    }

    public ResolvablePossibilitySet ResolveWithBaseEffectStack(GameState gameState, EffectStack effectStack)
    {
        return Resolve(gameState)
            .Select(resolvablePossibility => resolvablePossibility.WithBaseEffectStack(effectStack))
            .ToArray();
    }
}