using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState.DrawCard().Select(x => x.WithEffectStack()).ToArray();
    }
}