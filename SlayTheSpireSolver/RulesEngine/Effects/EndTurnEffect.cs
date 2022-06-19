namespace SlayTheSpireSolver.RulesEngine.Effects;

public class EndTurnEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        return new[] { gameState.WithEffectStack(new EffectStack(new IncrementTurnEffect())) };
    }
}