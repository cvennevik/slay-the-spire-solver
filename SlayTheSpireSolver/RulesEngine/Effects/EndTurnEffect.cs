namespace SlayTheSpireSolver.RulesEngine.Effects;

public class EndTurnEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var newGameState = gameState with { Turn = gameState.Turn.Number + 1 };
        return new[] { newGameState.WithEffectStack() };
    }
}