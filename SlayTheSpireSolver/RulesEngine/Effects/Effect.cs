namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);

    public ResolvablePossibilitySet ResolveWithBaseEffectStack(GameState gameState, EffectStack effectStack)
    {
        return Resolve(gameState)
            .Select(resolvablePossibility => resolvablePossibility.WithBaseEffectStack(effectStack))
            .ToArray();
    }
}