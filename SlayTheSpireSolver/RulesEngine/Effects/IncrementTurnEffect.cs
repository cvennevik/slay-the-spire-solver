namespace SlayTheSpireSolver.RulesEngine.Effects;

public record IncrementTurnEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        var newGameState = gameState with { Turn = gameState.Turn.Number + 1 };
        return new[] { newGameState.WithEffectStack() };
    }
}