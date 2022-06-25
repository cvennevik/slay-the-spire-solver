namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    ResolvablePossibilitySet Resolve(GameState gameState);

    ResolvablePossibilitySet ResolveWithBaseEffectStack(GameState gameState, EffectStack effectStack)
    {
        return Resolve(gameState)
            .Select(resolvablePossibility => resolvablePossibility.WithBaseEffectStack(effectStack))
            .ToArray();
    }
}