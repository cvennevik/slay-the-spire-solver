namespace SlayTheSpireSolver.RulesEngine.Effects;

public record IncrementTurnEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Turn = gameState.Turn.Number + 1 };
    }
}