namespace SlayTheSpireSolver.RulesEngine.Effects;

public record IncrementTurnEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Turn = gameState.Turn.Number + 1 };
    }
}