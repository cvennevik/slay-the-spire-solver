namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);

    public virtual IReadOnlyCollection<Possibility> NewResolve(GameState gameState)
    {
        return Resolve(gameState)
            .Select(resolvablePossibility =>
                new Possibility(resolvablePossibility.GameState, resolvablePossibility.Probability))
            .ToList();
    }

    public ResolvablePossibilitySet ResolveWithBaseEffectStack(GameState gameState, EffectStack effectStack)
    {
        return Resolve(gameState)
            .Select(resolvablePossibility => resolvablePossibility.WithBaseEffectStack(effectStack))
            .ToArray();
    }
}